using Configs.Weapons;
using PlayerController1.Utility;
using UnityEngine;

namespace Gameplay.Player_Controller_Scripts.Weapon_Controller_Scripts
{
    public class ConfigHolder : Singleton<ConfigHolder>
    {
        [SerializeField] private WeaponImpactConfig _weaponImpactConfig;
        [SerializeField] private WeaponDataConfig _weaponDataConfig;

        public WeaponImpactConfig WeaponImpactConfig => _weaponImpactConfig;
        public WeaponDataConfig WeaponDataConfig => _weaponDataConfig;
    }
}