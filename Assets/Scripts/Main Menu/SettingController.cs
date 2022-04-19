using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    [SerializeField] private Dropdown _graphicsSettings;
    [SerializeField] private Toggle _volumeActive;
    [SerializeField] private Slider _volumeSettings;
    private float _previousVolumeValue;
    

    private void Awake()
    {
        LoadAudioToggleSettings();
        LoadAudioSliderSettings();
        _volumeActive.onValueChanged.AddListener(OnVolumeToggleActive);
        _volumeSettings.onValueChanged.AddListener(OnVolumeSliderChanged);
        
        
        _graphicsSettings.options.Clear();
        var graphicsSettings = QualitySettings.names;
        foreach (var setting in graphicsSettings)
        {
            var newOption = new Dropdown.OptionData(setting);
            _graphicsSettings.options.Add(newOption);
        }
        _graphicsSettings.onValueChanged.AddListener(OnGraphicsChanged);
        LoadGraphicsSettings();
    }

    private void OnVolumeSliderChanged(float value)
    {
        PlayerPrefs.SetFloat("VolumeSliderLevel", value);
        AudioListener.volume = value;
    }

    private void OnVolumeToggleActive(bool active)
    {
        if (active)
        {
            _volumeSettings.value = _previousVolumeValue;
        }
        else 
        {
            _previousVolumeValue = _volumeSettings.value;
            _volumeSettings.value = 0;
        }
        PlayerPrefs.SetFloat("PreviousVolumeValue", _previousVolumeValue);
        PlayerPrefs.SetInt("VolumeActive", (active ? 1 : 0));
    }
    
    private void LoadAudioSliderSettings()
    {
        if (!PlayerPrefs.HasKey("VolumeSliderLevel"))
            return;
        var currentVolumeLevel = PlayerPrefs.GetFloat("VolumeSliderLevel");
        AudioListener.volume = currentVolumeLevel;
        _volumeSettings.value = currentVolumeLevel;
    }

    private void LoadAudioToggleSettings()
    {
        if (!PlayerPrefs.HasKey("VolumeActive") || !PlayerPrefs.HasKey("PreviousVolumeValue"))
            return;
        var volumeActive = PlayerPrefs.GetInt("VolumeActive");
        _previousVolumeValue = PlayerPrefs.GetFloat("PreviousVolumeValue");
        Debug.Log("previous: " + _previousVolumeValue);
        if (volumeActive == 1)
            _volumeActive.isOn = true;
        if (volumeActive == 0)
            _volumeActive.isOn = false;
    }

    private void OnGraphicsChanged(int value)
    {
        PlayerPrefs.SetInt("GraphicsSettings", value);
        _graphicsSettings.value = value;
    }

    private void LoadGraphicsSettings()
    {
        if (!PlayerPrefs.HasKey("GraphicsSettings"))
            return;
        _graphicsSettings.value = PlayerPrefs.GetInt("GraphicsSettings");
    }
    
}
