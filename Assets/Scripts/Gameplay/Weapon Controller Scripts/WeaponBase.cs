using System;
using System.Collections;
using System.Collections.Generic;
using Configs.Weapons;
using Gameplay.Player_Controller_Scripts.Weapon_Controller_Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] protected Camera _cam;
    [SerializeField] private WeaponName _weaponName;
    [SerializeField] private AudioSource _weaponAudio;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private float _rbForce = 20f;
    
    protected WeaponData CurrentWeaponData;
    protected Animator animator;
    protected bool _move;
    protected bool isReloading;
    protected int _bullets;
    protected int _clips;
    
    [Inject]
    private InputManager _inputManager;
    private float _baseSpread;
    private float _shootingSpread;
    private float _spreadDecreaseTime;
    private bool isBusy;
    private bool isFire;
    private float nextShotTime;
    private bool _run;
    private float _targetSpread;

    private Coroutine _reloadCoroutine;
    private Coroutine _drawHideCoroutine;

    public bool IsBusy => isBusy;
    public WeaponName WeaponName => CurrentWeaponData.WeaponName;
    public Sprite WeaponImage => CurrentWeaponData.WeaponImage;

    public event Action<int, int> OnChangedBullets;
    public event Action<WeaponBase, bool> OnWeaponActivationChanged;
    public event Action<float> OnSpreadChanged;
    public event Action OnEnemyHit;

    [Inject] private ConfigHolder _configHolder;
    
    protected void Awake()
    {
        animator = GetComponent<Animator>();
        CurrentWeaponData = _configHolder.WeaponDataConfig.GetWeaponDataByName(_weaponName);
        _targetSpread = CurrentWeaponData.MinSpread;
        _bullets = CurrentWeaponData.Bullets;
        _clips = CurrentWeaponData.Clips;
    }

    protected virtual void Start()
    {
        _inputManager.OnJumpInput += OnJump;
        _inputManager.OnMoveInput += OnMove;
        _inputManager.OnRunInput += OnRun;
        OnChangedBullets?.Invoke(_bullets, _clips);
    }

    #region Input

    private void OnMove(Vector2 moveInput)
    {
        _move = moveInput.magnitude > 0f;
        _baseSpread = _move ? CurrentWeaponData.WalkSpread : CurrentWeaponData.IdleSpread;
    }

    private void OnJump()
    {
    }

    private void OnRun(bool run)
    {
        _run = run;
        if (_run && _move)
        {
            _baseSpread = CurrentWeaponData.RunSpread;
        }
    }

    #endregion


    protected virtual void Update()
    {
        if (Input.GetMouseButton(0))
        {
            FireInternal();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isReloading || !isBusy)
            {
                _reloadCoroutine = StartCoroutine(ReloadInternal());
            }
        }
        
        MoveAnimation();
        SpreadLogic();
    }

    private void MoveAnimation()
    {
        animator.SetBool("Run", _move && _run);
    }

    protected virtual void Fire()
    {
        if(isReloading || _run || isBusy)
            return;
        
        if (_bullets <= 0)
        {
            _reloadCoroutine = StartCoroutine(ReloadInternal());
            return;
        }

        if (Time.time > nextShotTime)
        {
            FireRaycast();
            _muzzleFlash.Play();
            PlaySound(CurrentWeaponData.FireAudio);
            animator.SetTrigger("Fire");
            _bullets--;
            nextShotTime = Time.time + CurrentWeaponData.FireDelay;

            _shootingSpread += CurrentWeaponData.SpreadIncreaseStep;
            _spreadDecreaseTime = Time.time + CurrentWeaponData.StartDecreaseTime;
        }
    }

    protected void PlaySound(AudioClip clip)
    {
        _weaponAudio.clip = clip;
        _weaponAudio.Play();
    }

    protected void FireRaycast()
    {
        var impactsConfig = _configHolder.WeaponImpactConfig;
        var posX = Screen.width / 2f + Random.Range(-_shootingSpread, _shootingSpread);
        var posY = Screen.height / 2f + Random.Range(-_shootingSpread, _shootingSpread);
        var ray = _cam.ScreenPointToRay(new Vector3(posX, posY, 0f));
        if (Physics.Raycast(ray, out var hitInfo, CurrentWeaponData.FireDistance, CurrentWeaponData.FireMask))
        {
            var tag = hitInfo.transform.gameObject.tag;
            var impactVFX = impactsConfig.GetImpactByTag(tag);
            var impact = Instantiate(impactVFX.VFX, hitInfo.point,  Quaternion.LookRotation(hitInfo.normal, Vector3.up));
            if(tag == "Enemy")
                OnEnemyHit?.Invoke();
            var damageable = hitInfo.transform.GetComponent<IDamageable>();
            if (damageable != null)
            {
                var damage = Random.Range(CurrentWeaponData.MinDamage, CurrentWeaponData.MaxDamage);
                damageable.Health.ApplyDamage(damage);
            }
            var rb = hitInfo.transform.GetComponent<Rigidbody>();
            if (rb != null)
            {
                var forceVector = (rb.transform.position - transform.position).normalized;
                rb.AddForce(forceVector * _rbForce);
            }
            Destroy(impact.gameObject, impactsConfig.ImpactLifetime);
        }
    }

    private void FireInternal()
    {
        Fire();
        OnChangedBullets?.Invoke(_bullets, _clips);
    }

    private IEnumerator ReloadInternal()
    {
        yield return StartCoroutine(Reload());
        OnChangedBullets?.Invoke(_bullets, _clips);
    }

    protected virtual IEnumerator Reload()
    {
        var canReload = _bullets < CurrentWeaponData.BulletsPerClip && _clips > 0;
        if (canReload)
        {
            isReloading = true;
            PlaySound(CurrentWeaponData.ReloadAudio);
            animator.SetTrigger("Reload");
            yield return new WaitForSeconds(CurrentWeaponData.ReloadingTime);
            var toAdd = CurrentWeaponData.BulletsPerClip - _bullets;
            if (_clips >= toAdd)
            {
                _bullets += toAdd;
                _clips -= toAdd;
            }
            else
            {
                _clips = 0;
                _bullets += _clips;
            }

            _reloadCoroutine = null;
            isReloading = false;
        }
    }

    public void ForceStopReloading()
    {
        if (_reloadCoroutine != null)
        {
            StopCoroutine(_reloadCoroutine);
            _reloadCoroutine = null;
            isReloading = false;
        }
    }
    
    private void SpreadLogic()
    {
        if (Time.time >= _spreadDecreaseTime && _shootingSpread > 0f)
        {
            _shootingSpread -= CurrentWeaponData.SpreadDecreaseStep;
            if (_shootingSpread < 0f)
                _shootingSpread = 0f;
        }

        var spread = _baseSpread + _shootingSpread;
        spread = Mathf.Clamp(spread, CurrentWeaponData.MinSpread, CurrentWeaponData.MaxSpread);
        _targetSpread = Mathf.Lerp(_targetSpread, spread, CurrentWeaponData.FadeSpreadSpeed * Time.deltaTime);
        OnSpreadChanged?.Invoke(_targetSpread);
    }

    public void SetActive(bool draw)
    {
        if (draw)
        {
            OnEnableAction();
        }

        if(_drawHideCoroutine != null)
            StopCoroutine(_drawHideCoroutine);
        _drawHideCoroutine = StartCoroutine(PlayDrawHide(draw));
    }
    
    private IEnumerator PlayDrawHide(bool draw)
    {
        var triggerName = draw ? "Draw" : "Hide";
        var waitTime = draw ? CurrentWeaponData.DrawTime : CurrentWeaponData.HideTime;
        animator.SetTrigger(triggerName);
        isBusy = true;
        yield return new WaitForSeconds(waitTime);
        isBusy = false;
        _drawHideCoroutine = null;
        
        if (!draw)
        {
            OnDisableAction();
        }
        
    }

    private void OnDisableAction()
    {
        ForceStopReloading();
        OnWeaponActivationChanged?.Invoke(this, false);
    }

    private void OnEnableAction()
    {
        OnChangedBullets?.Invoke(_bullets, _clips);
        OnWeaponActivationChanged?.Invoke(this, true);
    }
}
