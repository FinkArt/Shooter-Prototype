using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PauseMenuActivation : MonoBehaviour
{
    [SerializeField] private Button _openPauseMenuButton;
    [SerializeField] private GameObject _pauseMenuBody;
    private bool Active => _pauseMenuBody.activeInHierarchy;

    private void Awake()
    {
        _openPauseMenuButton.onClick.AddListener(() => SetPauseMenuActive(!Active));
    }

    private void SetPauseMenuActive(bool active)
    {
        _pauseMenuBody.SetActive(active);
        if (!active)
            Time.timeScale = 1f;
        else
        {
            Time.timeScale = 0f;
        }
    }
    
    // [.] [.] . . . . . . . . . . .
    // [MainThread] [Logic]
    //Awake, Start, OnEnable OnDisable OnValidate, OnTriggerEnter/Exit/Stay, OnCollisionEnter/Exit/Stay OnMouseEnter/Down/Exit/Stay/Up/UpAsButton 
    //Update FixedUpdate LateUpdate OnDestroy
    //
    
}
