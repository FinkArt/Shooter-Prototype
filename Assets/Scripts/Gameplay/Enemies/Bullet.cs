using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 4f;

    private float _damage;
    
    void Start()
    {
        Destroy(gameObject, _lifeTime);    
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    private void OnCollisionEnter(Collision other)
    {
        var healthComponent = other.transform.GetComponent<HealthComponent>();
        if (healthComponent != null)
        {
            healthComponent.ApplyDamage(_damage);
        }
        Destroy(gameObject);
    }
}
