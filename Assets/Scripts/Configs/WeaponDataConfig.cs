using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Configs.Weapons
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "OurTools/WeaponConfig", order = 0)]
    public class WeaponDataConfig : ScriptableObject
    {
        [Header("Weapons:")]
        [Space] [SerializeField] private WeaponData[] WeaponData;

        public WeaponData GetWeaponDataByName(WeaponName weaponName)
        {
            foreach (var weaponData in WeaponData)
            {
                if (weaponData.WeaponName == weaponName)
                    return weaponData;
            }
            return null;
        }
    }

    [System.Serializable]
    public class WeaponData
    {
        public string NameWeapon;
        public WeaponName WeaponName;
        public Sprite WeaponImage;
        public int Bullets;
        public int BulletsPerClip;
        public int Clips;
        public float ReloadingTime;
        public float DrawTime;
        public float HideTime;
        public float FireDelay;
        public float FireDistance;
        public LayerMask FireMask;
        public ParticleSystem MuzzleFlash;
    }
    
}

