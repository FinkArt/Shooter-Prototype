using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Configs.Shop
{
    [CreateAssetMenu(fileName = "ShopConfig", menuName = "OurTools/ShopConfig", order = 1)]
    public class ShopConfig : ScriptableObject
    {
        public ShopItemView ItemViewPrefab;
        [Space] [Header("Shop items")] public CurrencyItem[] CoinsItems;
        [Space] public CurrencyItem[] CrystalsItems;
        [Space] public SwordData[] SwordItems;
        [Space] public AvatarData[] AvatarItems;
    }


    [System.Serializable]
    public class ShopItemBase
    {
        public string ItemName;
        public Sprite ItemIcon;
        public float Cost;
    }

    [System.Serializable]
    public class CurrencyItem : ShopItemBase
    {
        public int Amount;
    }

    [System.Serializable]
    public class SwordData : ShopItemBase
    {
        public float SwordLevel;
    }

    [System.Serializable]
    public class AvatarData : ShopItemBase
    {
        public float AvatarLevel;
    }

}
