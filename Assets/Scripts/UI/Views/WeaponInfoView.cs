using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoView : MonoBehaviour
{
   [SerializeField] private Text _bulletInputText;
   [SerializeField] private Text _weaponNameText;
   [SerializeField] private Image _weaponImage;

   public void SetWeaponName(string weaponName, Sprite weaponImage)
   {
      _weaponNameText.text = weaponName;
      _weaponImage.sprite = weaponImage;

   }

   public void SetBulletInfo(int bullets, int clips)
   {
      _bulletInputText.text = $"{bullets} / {clips}";
   }
   
}
