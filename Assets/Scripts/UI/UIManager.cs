using System.Collections;
using System.Collections.Generic;
using PlayerController1.Utility;
using UI.Presenters;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private WeaponInfoView _weaponInfoView;
    [SerializeField] private WeaponsManager _weaponsManager;
    [SerializeField] private List<WeaponBase> _weapon = new List<WeaponBase>();
    private WeaponInfoPresenter _weaponInfoPresenter;

    private void Start()
    {
        foreach (var weapon in _weapon)
        {
            _weaponInfoPresenter = new WeaponInfoPresenter(_weaponInfoView, _weaponsManager, weapon);
        }
        
    }
}