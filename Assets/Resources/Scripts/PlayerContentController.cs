using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum PlayerContentName
{
    coins =0,
    crystals = 1,
    swords = 2,
    armor =3
}

[System.Serializable]
public class PlayerContentItem
{
    public PlayerContentName playerContentName;
    public Text PlayerContentText;
    public Button AddContentButton;
    public Button RemoveContentButton;

    public void SetPlayerContent(int result)
    {
        PlayerContentText.text = result.ToString();
    }
}

public class PlayerResourceManager
{
    public Dictionary<PlayerContentName, int> Values = new Dictionary<PlayerContentName, int>();
    
    public PlayerResourceManager()
    {
        foreach (var contentType in Enum.GetNames(typeof(PlayerContentName)))
        {
            var type = (PlayerContentName) Enum.Parse(typeof(PlayerContentName), contentType);
            Values[type] = (PlayerPrefs.HasKey(type.ToString())) ? PlayerPrefs.GetInt(type.ToString()) : 0;
            //Values.ContainsKey()
        }
    }


}

public class PlayerContentController : MonoBehaviour
{
    [SerializeField] private PlayerContentItem[] PlayerContentItems;

    private PlayerResourceManager _playerResourceManager;
    
    private void Awake()
    {
        //AudioListener.volume = 0;
        
        _playerResourceManager = new PlayerResourceManager();

        foreach (var playerContent in PlayerContentItems)
        {
            RefreshContent(playerContent.playerContentName);
            playerContent.AddContentButton.onClick.AddListener( () => ChangeContent(playerContent.playerContentName, 100));
            playerContent.RemoveContentButton.onClick.AddListener(()=> ChangeContent(playerContent.playerContentName, -100));
        }
    }

    private PlayerContentItem GetContentName(PlayerContentName playerContentName)
    {
        foreach (var playerContentItem in PlayerContentItems)
        {
            if (playerContentItem.playerContentName == playerContentName)
                return playerContentItem;
        }

        return null;
    }

    private void RefreshContent(PlayerContentName playerContentName)
    {
        ChangeContent(playerContentName, 0);
    }
    
    private void ChangeContent(PlayerContentName playerCurrentContentName, int extra)
    {
        _playerResourceManager.Values[playerCurrentContentName] += extra;

        var resourcesResult = _playerResourceManager.Values[playerCurrentContentName];
        var currentContantName = GetContentName(playerCurrentContentName);
        currentContantName.SetPlayerContent(resourcesResult);
        Debug.Log($"{playerCurrentContentName} = {resourcesResult}");
        PlayerPrefs.SetInt(playerCurrentContentName.ToString(), resourcesResult);
    }
}
