using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Gameplay.Player_Controller_Scripts.Weapon_Controller_Scripts;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponName
{
    Mashinegun = 0,
    ShotGun = 1,
    Gun = 2,
    None = 1000
}

public interface IWeaponsManager
{
    event Action<string, Sprite> OnWeaponChanged;
    event Action<int, int> OnWeaponAmmoInfoChanged;
}


public class WeaponsManager : MonoBehaviour, IWeaponsManager
{
    [SerializeField] private List<WeaponBase> _weapons = new List<WeaponBase>();
    [Space]
    [SerializeField] private int _startWeaponIdx;

    private InputManager _inputManager;
    
    private int? _currentWeaponIdx;
    private int? _prevWeaponIdx;


    public event Action<int, int> OnWeaponAmmoInfoChanged;
    public event Action<string, Sprite> OnWeaponChanged;
    
    private Coroutine _changeWeaponCoroutine;

    private bool _isChangeWeapon;
    

    private void Start()
    {
        SelectWeapon(_startWeaponIdx);
        _inputManager = InputManager.Instance;
        _inputManager.OnWeaponChangeKeyInput += SelectWeapon;
        _inputManager.OnPrevWeaponInput += SelectPreviousWeapon;
        _inputManager.OnWeaponChangeScrollMouseInput += ScrollMouse;
    }
    
    private int GetWeaponIdxWithShift(int shift)
    {
        if (!_currentWeaponIdx.HasValue)
        {
            return _startWeaponIdx;
        }
        var idx = _currentWeaponIdx.Value;
        idx += shift;
        if (idx < 0)
            idx = _weapons.Count - 1;
        else if (idx > _weapons.Count - 1)
            idx = 0;

        return idx;
    }

    private void ScrollMouse(float mouseInput)
    {
        if (mouseInput > 0f)
        {
            var nextIdx = GetWeaponIdxWithShift(1);
            SelectWeapon(nextIdx);
        }
        else if(mouseInput < 0f)
        {
            var prevIdx = GetWeaponIdxWithShift(-1);
            SelectWeapon(prevIdx);
        }
    }

    private void SelectPreviousWeapon()
    {
        if(_prevWeaponIdx.HasValue)
            SelectWeapon(_prevWeaponIdx.Value);
    }
    
    private void SelectWeapon(int id)
    {
        if (!_isChangeWeapon)
        {
            if(_changeWeaponCoroutine != null)
                StopCoroutine(_changeWeaponCoroutine);
            _changeWeaponCoroutine = StartCoroutine(ChangeWeapon(id));
        }
    }

    private IEnumerator ChangeWeapon(int idx)
    {
        if (_currentWeaponIdx.HasValue && idx == _currentWeaponIdx.Value)
            yield break;
        
        var current = _weapons[idx];
        if (_currentWeaponIdx.HasValue)
        {
            _isChangeWeapon = true;
            _prevWeaponIdx = _currentWeaponIdx;
            _currentWeaponIdx = idx;
            
            var previous = _weapons[_prevWeaponIdx.Value];
            previous.gameObject.SetActive(true);
            previous.SetActive(false);
            
            yield return new WaitUntil(() => previous.IsBusy == false);
            
            previous.gameObject.SetActive(false);
            current.gameObject.SetActive(true);
            
            ActivateWeapon(_currentWeaponIdx.Value);
            
            yield return new WaitUntil(() => current.IsBusy == false);
        }
        else
        {
            current.gameObject.SetActive(true);
            _currentWeaponIdx = idx;
            
            ActivateWeapon(_currentWeaponIdx.Value);
        }
        
        _changeWeaponCoroutine = null;
        _isChangeWeapon = false;
    }

    private void ActivateWeapon(int idx)
    {
        var current = _weapons[idx];
        current.OnChangedBullets += OnCurrentWeaponInfoChanged;
        current.SetActive(true);
        OnWeaponChanged?.Invoke(current.WeaponName.ToString(), current.WeaponImage);
        
    }

    private void OnCurrentWeaponInfoChanged(int bullets, int clips)
    {
        OnWeaponAmmoInfoChanged?.Invoke(bullets, clips);
    }
    
}
