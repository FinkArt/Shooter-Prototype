using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerHealthView _playerHealthView;

    private void Awake()
    {
        _playerController.OnPlayerSpawned += (player) => FindHealthBar(player.Health);
    }

    private void FindHealthBar(HealthComponent healthComponent)
    {
        healthComponent.OnHealthChanged += OnHealthChanged;
        
    }

    private void OnHealthChanged(float health, float maxHealth)
    {
        _playerHealthView.SetHealth(health / maxHealth);
    }
    
}
