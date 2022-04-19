using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// public enum PauseMenuName
// {
//     MainMenu = 0,
//     Settings = 1,
//     None = 500
// }
//
// [System.Serializable]
// public class PauseMenuMode
// {
//     public PauseMenuName PauseMenuName;
//     
//     public Button PauseMenuButton;
//     
// }
public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private Button _mainMenuButton;
    //[SerializeField] private PauseMenuMode _pauseMenuMode;
    [SerializeField] private string _playSceneName = "Menu";
    private void Awake()
    {
        _mainMenuButton.onClick.AddListener(() => GoBackToMainMenu());
    }

    private void GoBackToMainMenu()
    {
        SceneManager.LoadScene(_playSceneName);
    }
    
    
}
