using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

    public enum ShopItemType
    {
        Coins = 0,
        Crystals = 1,
        Avatars = 2,
        Swords = 3,
        None = 1000,
    }

    [System.Serializable]
    public class ShopTab
    {
        public ShopItemType shopItemType;
        public string ShopTabName;
        public Button TabButton;
        public Image TabImage;
        public Text TabText;
        public GameObject ShopSectionView;
        public Sprite ActiveButtonSprite;
        public Sprite DisableButtonSprite;
        public Color ActiveTextButtonColor;
        public Color DisableTextButtonColor;
        public RectTransform ContentHolder;

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
        [SerializeField] private ShopConfig _shopConfig;
        [SerializeField] private ShopTab[] _shopTabs;
        [SerializeField] private ShopItemType _startTabName;
        
        private ShopItemType _currentShopItemType = ShopItemType.None;
        
        private void Awake()
        {
            
            foreach (var shopTab in _shopTabs)
            {
                shopTab.SetButtonTitle();
                if (shopTab.TabButton != null)
                    shopTab.TabButton.onClick.AddListener(() => SelectShopMode(shopTab.shopItemType));
            }
            InitShop();
            SelectShopMode(_startTabName);
        }

        private void InitShop()
        {
            foreach (var shopTab in _shopTabs)
            {
                var viewPrefab = _shopConfig.GetItemView(shopTab.shopItemType);
                switch (shopTab.shopItemType)
                {
                    case ShopItemType.Coins:
                        var coinsItems = _shopConfig.CoinsItems;
                        foreach (var coinsItem in coinsItems)
                        {
                            var newTabObject = Instantiate(viewPrefab, shopTab.ContentHolder);
                        }
                        break;
                    case ShopItemType.Avatars:
                        var avatarsItems = _shopConfig.AvatarItems;
                        foreach (var avatarsItem in avatarsItems)
                        {
                            var newTabObject = Instantiate(viewPrefab, shopTab.ContentHolder);
                        }
                        break;
                    case ShopItemType.Crystals:
                        var crystalsItems = _shopConfig.CrystalsItems;
                        foreach (var crystalsItem in crystalsItems)
                        {
                            var newTabObject = Instantiate(viewPrefab, shopTab.ContentHolder);
                        }
                        break;
                    case ShopItemType.Swords:
                        var swordsItems = _shopConfig.SwordItems;
                        foreach (var swordItem in swordsItems)
                        {
                            var newTabObject = Instantiate(viewPrefab, shopTab.ContentHolder);
                        }
                        break;
                }
            }
        }

        private ShopTab GetShopSectionByName(ShopItemType shopItemType)
        {
            for (int i = 0; i < _shopTabs.Length; i++)
            {
                if (_shopTabs[i].shopItemType == shopItemType)
                    return _shopTabs[i];
            }
            return null;
        }


        private void SelectShopMode(ShopItemType shopItemType)
        {
            if (_currentShopItemType == shopItemType)
                return;

            var previousShopSection = GetShopSectionByName(_currentShopItemType);
            if (previousShopSection != null)
            {
                previousShopSection.SetShopSectionStatus(false);
                previousShopSection.SetActiveButton(false);
            }

            _currentShopItemType = shopItemType;
            var openedShopSection = GetShopSectionByName(_currentShopItemType);
            if (openedShopSection != null)
            {
                openedShopSection.SetShopSectionStatus(true);
                openedShopSection.SetActiveButton(true);
            }
        }

    }

