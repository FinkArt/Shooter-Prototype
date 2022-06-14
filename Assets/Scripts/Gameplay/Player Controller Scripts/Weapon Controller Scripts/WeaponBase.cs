using System;
using System.Collections;
using System.Collections.Generic;
using Configs.Weapons;
using Gameplay.Player_Controller_Scripts.Weapon_Controller_Scripts;
using UnityEngine;
using UnityEngine.UI;

public abstract class WeaponBase : MonoBehaviour
{
    
    [SerializeField] protected Camera _cam;
    [SerializeField] private WeaponName _weaponName;
    
    protected WeaponData CurrentWeaponData;
    
    protected Animator animator;
    protected InputManager _inputManager;

   

    public bool IsBusy => isBusy; 
    
    protected bool isReloading;
    protected bool isBusy;
    protected float nextShotTime;
    protected bool _move;
    private bool _run;
    private Coroutine _reloadCoroutine;
    private Coroutine _drawHideCoroutine;

    public WeaponName WeaponName => CurrentWeaponData.WeaponName;
    public Sprite WeaponImage => CurrentWeaponData.WeaponImage;

    public event Action<int, int> OnChangedBullets;
    public event Action<WeaponBase, bool> OnWeaponActivationChanged;
    
    protected void Awake()
    {
        animator = GetComponent<Animator>();
        CurrentWeaponData = ConfigHolder.Instance.WeaponDataConfig.GetWeaponDataByName(_weaponName);
    }

    protected virtual void Start()
    {
        _inputManager = InputManager.Instance;
        _inputManager.OnJumpInput += OnJump;
        _inputManager.OnMoveInput += OnMove;
        _inputManager.OnRunInput += OnRun;
        OnChangedBullets?.Invoke(CurrentWeaponData.Bullets, CurrentWeaponData.Clips);
    }

    #region Input

    private void OnMove(Vector2 moveInput)
    {
        _move = moveInput.magnitude > 0f;
        
    }

    private void OnJump()
    {
    }

    private void OnRun(bool run)
    {
        _run = run;
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
    }

    private void MoveAnimation()
    {
        animator.SetBool("Run", _move && _run);
    }

    protected virtual void Fire()
    {
        if(isReloading || _run || isBusy)
            return;
        
        if (CurrentWeaponData.Bullets <= 0)
        {
            _reloadCoroutine = StartCoroutine(ReloadInternal());
            return;
        }

        if (Time.time > nextShotTime)
        {
            FireRaycast();
            CurrentWeaponData.MuzzleFlash.Play();
            animator.SetTrigger("Fire");
            CurrentWeaponData.Bullets--;
            nextShotTime = Time.time + CurrentWeaponData.FireDelay;
        }
    }

    protected void FireRaycast()
    {
        var impactsConfig = ConfigHolder.Instance.WeaponImpactConfig;
        var ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out var hitInfo, CurrentWeaponData.FireDistance, CurrentWeaponData.FireMask))
        {
            var tag = hitInfo.transform.gameObject.tag;
            var impactVFX = impactsConfig.GetImpactByTag(tag);
            var impact = Instantiate(impactVFX.VFX, hitInfo.point,  Quaternion.LookRotation(hitInfo.normal, Vector3.up));
            Destroy(impact.gameObject, impactsConfig.ImpactLifetime);
        }
    }

    private void FireInternal()
    {
        Fire();
        OnChangedBullets?.Invoke(CurrentWeaponData.Bullets, CurrentWeaponData.Clips);
    }

    private IEnumerator ReloadInternal()
    {
        yield return StartCoroutine(Reload());
        OnChangedBullets?.Invoke(CurrentWeaponData.Bullets, CurrentWeaponData.Clips);
    }

    protected virtual IEnumerator Reload()
    {
        var canReload = CurrentWeaponData.Bullets < CurrentWeaponData.BulletsPerClip && CurrentWeaponData.Clips > 0;
        if (canReload)
        {
            isReloading = true;
            animator.SetTrigger("Reload");
            yield return new WaitForSeconds(CurrentWeaponData.ReloadingTime);
            var toAdd = CurrentWeaponData.BulletsPerClip - CurrentWeaponData.Bullets;
            if (CurrentWeaponData.Clips >= toAdd)
            {
                CurrentWeaponData.Bullets += toAdd;
                CurrentWeaponData.Clips -= toAdd;
            }
            else
            {
                CurrentWeaponData.Clips = 0;
                CurrentWeaponData.Bullets += CurrentWeaponData.Clips;
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
        OnChangedBullets?.Invoke(CurrentWeaponData.Bullets, CurrentWeaponData.Clips);
        OnWeaponActivationChanged?.Invoke(this, true);
    }
}
