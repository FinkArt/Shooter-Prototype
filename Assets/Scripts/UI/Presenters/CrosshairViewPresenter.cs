using UI.Views;
using UnityEngine;

namespace UI.Presenters
{
    public class CrosshairViewPresenter
    {
        private CrosshairView _crosshairView;
        private WeaponsManager _weaponsManager;
        private WeaponBase _currentWeapon;

        public CrosshairViewPresenter(CrosshairView crosshairView, WeaponsManager weaponsManager)
        {
            _crosshairView = crosshairView;
            _weaponsManager = weaponsManager;
            
            
            _weaponsManager.OnWeaponChanged += WeaponsManagerOnOnWeaponChanged;
        }

        private void WeaponsManagerOnOnWeaponChanged(WeaponBase weapon)
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.OnSpreadChanged -= OnWeaponSpreadChanged;
                _currentWeapon.OnEnemyHit -= OnWeaponHit;
            }
            _currentWeapon = weapon;
            _currentWeapon.OnSpreadChanged += OnWeaponSpreadChanged;
            _currentWeapon.OnEnemyHit += OnWeaponHit;
        }

        private void OnWeaponSpreadChanged(float spread)
        {
            _crosshairView.SetSpread(spread);
        }
        
        private void OnWeaponHit()
        {
            _crosshairView.OnHit();
        }
    }
}