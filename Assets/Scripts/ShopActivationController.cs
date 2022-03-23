using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


   public class ShopActivationController : MonoBehaviour
   {
      [SerializeField] private MenuController _menuController;
      [SerializeField] private GameObject _shopBody;
      [SerializeField] private Button _openShopButton;
      [SerializeField] private Button _closeShopButton;
      private bool Active => _shopBody.activeInHierarchy;

      private void Awake()
      {
         _closeShopButton.onClick.AddListener(() => SetShopActive(false));
         _openShopButton.onClick.AddListener(() => SetShopActive(!Active));
      }

      private void SetShopActive(bool active)
      {
         _shopBody.SetActive(active);
         if (active)
            _menuController.SelectMenuMode(MenuName.None);
         else
         {
            _menuController.SelectMenuMode(MenuName.MainMenu);
         }
      }
   }
