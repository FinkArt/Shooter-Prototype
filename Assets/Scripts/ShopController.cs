using System;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [SerializeField] private Button _crystalsButton;
    [SerializeField] private Button _coinsButton;
    [SerializeField] private Button _avatarsButton;
    [SerializeField] private Button _swordsButton;
    [Space] 
    [SerializeField] private GameObject _crystalsViewport;
    [SerializeField] private GameObject _coinsViewport;
    [SerializeField] private GameObject _avatarsViewport;
    [SerializeField] private GameObject _swordsViewport;
    [Space] 
    [SerializeField] private Sprite _activeButtonSprite;
    [SerializeField] private Sprite _disactiveButtonSprite;
    [Space]
    [SerializeField] private Color _brownColor;
    [SerializeField] private Color _yellowColor;

    private void Awake()
    {
        _crystalsButton.onClick.AddListener(OnCrystalsButtonClick);
        _coinsButton.onClick.AddListener(OnCoinsButtonClick);
        _avatarsButton.onClick.AddListener(OnAvatarsButtonClick);
        _swordsButton.onClick.AddListener(OnSwordsButtonClick);
        
    }

    private void OnCrystalsButtonClick()
    {
        _crystalsViewport.SetActive(true);
        _coinsViewport.SetActive(false);
        _avatarsViewport.SetActive(false);
        _swordsViewport.SetActive(false);
        _crystalsButton.GetComponent<Image>().sprite = _activeButtonSprite;
        _crystalsButton.GetComponentInChildren<Text>().color = _brownColor;
        _coinsButton.GetComponent<Image>().sprite = _disactiveButtonSprite;
        _coinsButton.GetComponentInChildren<Text>().color = _yellowColor;
        _avatarsButton.GetComponent<Image>().sprite = _disactiveButtonSprite;
        _avatarsButton.GetComponentInChildren<Text>().color = _yellowColor;
        _swordsButton.GetComponent<Image>().sprite = _disactiveButtonSprite;
        _swordsButton.GetComponentInChildren<Text>().color = _yellowColor;
        
    }

    private void OnCoinsButtonClick()
    {
        _crystalsViewport.SetActive(false);
        _coinsViewport.SetActive(true);
        _avatarsViewport.SetActive(false);
        _swordsViewport.SetActive(false);
        _crystalsButton.GetComponent<Image>().sprite = _disactiveButtonSprite;
        _crystalsButton.GetComponentInChildren<Text>().color = _yellowColor;
        _coinsButton.GetComponent<Image>().sprite = _activeButtonSprite;
        _coinsButton.GetComponentInChildren<Text>().color = _brownColor;
        _avatarsButton.GetComponent<Image>().sprite = _disactiveButtonSprite;
        _avatarsButton.GetComponentInChildren<Text>().color = _yellowColor;
        _swordsButton.GetComponent<Image>().sprite = _disactiveButtonSprite;
        _swordsButton.GetComponentInChildren<Text>().color = _yellowColor;
    }

    private void OnAvatarsButtonClick()
    {
        _crystalsViewport.SetActive(false);
        _coinsViewport.SetActive(false);
        _avatarsViewport.SetActive(true);
        _swordsViewport.SetActive(false);
        _crystalsButton.GetComponent<Image>().sprite = _disactiveButtonSprite;
        _crystalsButton.GetComponentInChildren<Text>().color = _yellowColor;
        _coinsButton.GetComponent<Image>().sprite = _disactiveButtonSprite;
        _coinsButton.GetComponentInChildren<Text>().color = _yellowColor;
        _avatarsButton.GetComponent<Image>().sprite = _activeButtonSprite;
        _avatarsButton.GetComponentInChildren<Text>().color = _brownColor;
        _swordsButton.GetComponent<Image>().sprite = _disactiveButtonSprite;
        _swordsButton.GetComponentInChildren<Text>().color = _yellowColor;
    }

    private void OnSwordsButtonClick()
    {
        _crystalsViewport.SetActive(false);
        _coinsViewport.SetActive(false);
        _avatarsViewport.SetActive(false);
        _swordsViewport.SetActive(true);
        _crystalsButton.GetComponent<Image>().sprite = _disactiveButtonSprite;
        _crystalsButton.GetComponentInChildren<Text>().color = _yellowColor;
        _coinsButton.GetComponent<Image>().sprite = _disactiveButtonSprite;
        _coinsButton.GetComponentInChildren<Text>().color = _yellowColor;
        _avatarsButton.GetComponent<Image>().sprite = _disactiveButtonSprite;
        _avatarsButton.GetComponentInChildren<Text>().color = _yellowColor;
        _swordsButton.GetComponent<Image>().sprite = _activeButtonSprite;
        _swordsButton.GetComponentInChildren<Text>().color = _brownColor;
        
    }
    
}
