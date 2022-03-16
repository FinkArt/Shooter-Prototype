using System;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShopTab
{
    public string ShopTabName;
    public Button TabButton;
    public Image TabImage;
    public Text TabText;
    public GameObject ShopTabItemViewPrefab;
    public RectTransform SpawnRoot;
    public GameObject View;

    public void SetTitle()
    {
        TabText.text = ShopTabName;
    }

    public void SetTabSprite(Sprite sprite)
    {
        TabImage.sprite = sprite;
    }

    public void SetTextColor(Color color)
    {
        TabText.color = color;
    }
}

public class ShopController : MonoBehaviour
{
    [SerializeField] private GameObject _shopBody;
    [SerializeField] private ShopTab[] _shopTabs;
    [Space]
    [SerializeField] private Sprite _activeTabSprite;
    [SerializeField] private Sprite _disabledTabSprite;
    [Space]
    [SerializeField] private Color _activeTextColor;
    [SerializeField] private Color _disabledTextColor;
    [Space]
    [SerializeField] private Button _closeButton;
    public bool Active => _shopBody.activeInHierarchy;

    private ShopTab _currentTab; //ссылка указатель на активную вкладку
    

    private void Awake()
    {
        _closeButton.onClick.AddListener(() => SetShopActive(false));

        foreach (var shopTab in _shopTabs)
        {
            shopTab.SetTitle();
            shopTab.TabButton.onClick.AddListener(() => OnTabClicked(shopTab));
        }
    }

    private void OnTabClicked(ShopTab tab)
    {
        if (_currentTab != null)
        {
            _currentTab.SetTabSprite(_disabledTabSprite);
            _currentTab.View.SetActive(false);
            _currentTab.SetTextColor(_disabledTextColor);
            
        }

        tab.SetTabSprite(_activeTabSprite);
        tab.View.SetActive(true);
        tab.SetTextColor(_activeTextColor);
        
        _currentTab = tab;
    }

    public void SetShopActive(bool active)
    {
        _shopBody.SetActive(active);
    }
}
