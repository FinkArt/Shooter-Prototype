using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour
{
    [SerializeField] private Image _healthBarLine;
    
    public void SetHealth(float health)
    {
        _healthBarLine.fillAmount = health;
    }
    
}
