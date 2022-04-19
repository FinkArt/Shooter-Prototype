using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private bool _isDead;
    [SerializeField] private bool _useRegeneration;
    [SerializeField] private float _healthRegenerationAmount = 5f;
    [SerializeField] private float _regenerationDelay = 0.5f;
    [SerializeField] private float _onDamageRegenerationDelay = 3f;
    
    private float _nextRegenerationTime;

    private float _hp
    {
        get
        {
            return _health;
        }

        set
        {
            _health = value;
            OnHealthChanged?.Invoke(_health, _maxHealth);
        }
    }

    public event Action<float, float> OnHealthChanged;
    public event Action OnDead; 
    
    private void Awake()
    {
        _health = _maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        if(_isDead)
            return;

        if (_useRegeneration)
        {
            _nextRegenerationTime = Time.time + _onDamageRegenerationDelay;
        }
        _hp -= damage;
        if (_health <= 0f)
        {
            _health = 0f;
            Dead();
            OnDead?.Invoke();
        }
    }

    private void Update()
    {
        if(_isDead)
            return;
        
        if (_useRegeneration && _health < _maxHealth)
        {
            if (Time.time > _nextRegenerationTime)
            {
                _hp = _hp + _healthRegenerationAmount;
                if (_health > _maxHealth)
                    _health = _maxHealth;

                _nextRegenerationTime = Time.time + _regenerationDelay;
            }
        }
    }

    private void Dead()
    {
        _isDead = true;
    }
}
