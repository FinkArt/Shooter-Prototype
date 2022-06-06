using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] protected WeaponName _weaponName;
    //[SerializeField] protected string weaponName;
    [SerializeField] protected Sprite weaponImage;
    [SerializeField] protected int bullets = 30;
    [SerializeField] protected int bulletsPerClip = 30;
    [SerializeField] protected int clips = 120;
    [SerializeField] protected float _reloadingTime = 2;
    [SerializeField] protected float _drawTime = 1f;
    [SerializeField] protected float _hideTime = 1;
    [SerializeField] protected float _fireDelay = 0.2f;
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

    public WeaponName WeaponName => _weaponName;
    public Sprite WeaponImage => weaponImage;

    public event Action<int, int> OnChangedBullets;
    public event Action<WeaponBase, bool> OnWeaponActivationChanged;
    
    protected void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        _inputManager = InputManager.Instance;
        _inputManager.OnJumpInput += OnJump;
        _inputManager.OnMoveInput += OnMove;
        _inputManager.OnRunInput += OnRun;
        OnChangedBullets?.Invoke(bullets, clips);
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
        
        if (bullets <= 0)
        {
            _reloadCoroutine = StartCoroutine(ReloadInternal());
            return;
        }

        if (Time.time > nextShotTime)
        {
            animator.SetTrigger("Fire");
            bullets--;
            nextShotTime = Time.time + _fireDelay;
        }
    }

    private void FireInternal()
    {
        Fire();
        OnChangedBullets?.Invoke(bullets, clips);
    }

    private IEnumerator ReloadInternal()
    {
        yield return StartCoroutine(Reload());
        OnChangedBullets?.Invoke(bullets, clips);
    }

    protected virtual IEnumerator Reload()
    {
        var canReload = bullets < bulletsPerClip && clips > 0;
        if (canReload)
        {
            isReloading = true;
            animator.SetTrigger("Reload");
            yield return new WaitForSeconds(_reloadingTime);
            var toAdd = bulletsPerClip - bullets;
            if (clips >= toAdd)
            {
                bullets += toAdd;
                clips -= toAdd;
            }
            else
            {
                clips = 0;
                bullets += clips;
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
        var waitTime = draw ? _drawTime : _hideTime;
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
        OnChangedBullets?.Invoke(bullets, clips);
        OnWeaponActivationChanged?.Invoke(this, true);
    }
}
