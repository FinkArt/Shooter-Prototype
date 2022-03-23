using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

    public enum ShopSectionName
    {
        Coins = 0,
        Crystals = 1,
        Avatars = 2,
        Swords = 3
    }

    [System.Serializable]
    public class ShopTab
    {
        public ShopSectionName ShopSectionName;
        public string ShopTabName;
        public Button TabButton;
        public Image TabImage;
        public Text TabText;
        public GameObject ShopSectionView;
        public Sprite ActiveButtonSprite;
        public Sprite DisableButtonSprite;
        public Color ActiveTextButtonColor;
        public Color DisableTextButtonColor;

        public void SetActiveButton(bool active)
        {
            if (active)
            {
                if (ActiveButtonSprite == null || ActiveTextButtonColor == null)
                    return;
                TabImage.sprite = ActiveButtonSprite;
                TabText.color = ActiveTextButtonColor;
            }
            else
            {
                if (DisableButtonSprite == null || DisableTextButtonColor == null)
                    return;
                TabImage.sprite = DisableButtonSprite;
                TabText.color = DisableTextButtonColor;
            }
        }
        public void SetButtonTitle()
        {
            if (TabText == null)
                return;
            TabText.text = ShopTabName;
        }
        public void SetShopSectionStatus(bool isVisible)
        {
            if(ShopSectionView == null)
                return;
            ShopSectionView.SetActive(isVisible); 
        }
    }

    public class ShopSectionController : MonoBehaviour
    {
        [SerializeField] private ShopTab[] _shopTabs;
        [SerializeField] private ShopSectionName _currentShopSectionName;
        private void Awake()
        {
            
            foreach (var shopTab in _shopTabs)
            {
                shopTab.SetButtonTitle();
                if (shopTab.TabButton != null)
                    shopTab.TabButton.onClick.AddListener(() => SelectShopMode(shopTab.ShopSectionName));
            }
            SelectShopMode(ShopSectionName.Crystals);
        }

        private ShopTab GetShopSectionByName(ShopSectionName shopSectionName)
        {
            for (int i = 0; i < _shopTabs.Length; i++)
            {
                if (_shopTabs[i].ShopSectionName == shopSectionName)
                    return _shopTabs[i];
            }
            return null;
        }

        private void SelectShopSection(bool active)
        {
            var shopSection = GetShopSectionByName(_currentShopSectionName);
            if (shopSection != null)
            {
                shopSection.SetShopSectionStatus(active);
                shopSection.SetActiveButton(active);
            }
        }

        private void SelectShopMode(ShopSectionName shopSectionName)
        {
            if (_currentShopSectionName == shopSectionName)
                return;
            if (_currentShopSectionName != null)
            {
                SelectShopSection(false);
            }
            _currentShopSectionName = shopSectionName;
            SelectShopSection(true);
        }
        
    }

