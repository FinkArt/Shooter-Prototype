using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour
{
    [SerializeField] private Image _healthBarLine;

    private RectTransform _rt;

    private void Awake()
    {
        _rt = transform as RectTransform;
    }

    public void SetPosition(Vector3 newPosition)
    {
        _rt.position = newPosition;
    }

    public void SetHealth(float health)
    {
        _healthBarLine.fillAmount = health;
    }
}
