using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent _navMesh;

    [SerializeField] private HealthComponent _healthComponent;

    private PlayerController _target;
    public HealthComponent Health => _healthComponent;

    [SerializeField] private float _minDamage = 5f;
    [SerializeField] private float _maxDamage = 15f;
    [SerializeField] private float _attackDistance = 2f;
    [SerializeField] private float _attackDelay;
    private Animator _animator;

    private float _nextAttackTime;

    private void Start()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

    }

    private void FindTarget()
    {
        _target = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (_target == null)
        {
            FindTarget();
            return;
        }

        var distanceToPlayer = Vector3.Distance(transform.position, _target.transform.position);
        if (distanceToPlayer <= _attackDistance)
        {
            Attack();
        }
        _navMesh.SetDestination(_target.transform.position);

    }

    private void Attack()
    {
        if (Time.time >= _nextAttackTime)
        {
            _animator.SetTrigger("Attack");
            var damage = Random.Range(_minDamage, _maxDamage);
            _target.Health.ApplyDamage(damage);
            _nextAttackTime = Time.time + _attackDelay;
        }
          
        
    }
}
