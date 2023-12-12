using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PlayerController1.Utility;
using UI.Presenters;
using UI.Views;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


public class UIManager : MonoBehaviour
{
    [SerializeField] private WeaponInfoView _weaponInfoView;
    [SerializeField] private WeaponsManager _weaponsManager;
    [SerializeField] private List<WeaponBase> _weapon = new List<WeaponBase>();
    [SerializeField] private CrosshairView _crosshairView;
    private List<WeaponInfoPresenter> _weaponInfoPresenters = new List<WeaponInfoPresenter>();
    private CrosshairViewPresenter _crosshairViewPresenter;

    private void Start()
    {
        foreach (var weapon in _weapon)
        {
            var presenter = new WeaponInfoPresenter(_weaponInfoView, _weaponsManager, weapon);
            _weaponInfoPresenters.Add(presenter);
        }
        
        _crosshairViewPresenter = new CrosshairViewPresenter(_crosshairView, _weaponsManager);
    }
}