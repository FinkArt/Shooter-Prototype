using System.Collections.Generic;
using UnityEngine;

namespace UI.Presenters
{
    public class WeaponInfoPresenter
    {
        private WeaponInfoView _weaponInfoView;
        private IWeaponsManager _weaponsManager;
        
        public WeaponInfoPresenter(WeaponInfoView weaponInfoView, IWeaponsManager weaponsManager, WeaponBase weaponBase)
        {
            _weaponInfoView = weaponInfoView;
            _weaponsManager = weaponsManager;
            
            weaponBase.OnChangedBullets += WeaponsManagerOnOnWeaponAmmoInfoChanged;
            _weaponsManager.OnWeaponAmmoInfoChanged += WeaponsManagerOnOnWeaponAmmoInfoChanged;
            _weaponsManager.OnWeaponChanged += OnWeaponsManagerWeaponChanged;
        }

        private void WeaponsManagerOnOnWeaponAmmoInfoChanged(int bullets, int clips)
        {
            _weaponInfoView.SetBulletInfo(bullets, clips);
        }

        private void OnWeaponsManagerWeaponChanged(WeaponBase weapon)
        {
            _weaponInfoView.SetWeaponName(weapon.WeaponName.ToString(), weapon.WeaponImage);
        }
    }

}