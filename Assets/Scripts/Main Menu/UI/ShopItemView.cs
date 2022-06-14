using System.Collections;
using System.Collections.Generic;
using Configs.Weapons;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemView : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Text _itemDescription;
    [SerializeField] private Text _itemAmount;
    [SerializeField] private Text _itemPrice;

    public void ApplyCurrencyItemData(CurrencyItem currencyItem)
    {
        _itemAmount.text = currencyItem.Amount.ToString();
        _itemDescription.text = currencyItem.ItemName.ToString();
        _itemPrice.text = currencyItem.Cost.ToString();
        _itemImage.sprite = currencyItem.ItemIcon;
    }
    
    public void ApplySwordData(SwordData swordData)
    {
        _itemAmount.text = swordData.SwordLevel.ToString();
        _itemDescription.text = swordData.ItemName.ToString();
        _itemPrice.text = swordData.Cost.ToString();
        _itemImage.sprite = swordData.ItemIcon;
    }
    
    public void ApplyAvatarsData(AvatarData avatarData)
    {
        _itemAmount.text = avatarData.AvatarLevel.ToString();
        _itemDescription.text = avatarData.ItemName.ToString();
        _itemPrice.text = avatarData.Cost.ToString();
        _itemImage.sprite = avatarData.ItemIcon;
    }
    
    
}
