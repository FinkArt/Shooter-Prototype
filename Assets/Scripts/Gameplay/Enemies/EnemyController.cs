using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public interface IDamageable
{
    HealthComponent Health { get; }
}

public class EnemyController : MonoBehaviour, IDamageable
{
    private NavMeshAgent _navMesh;

    [SerializeField] private HealthComponent _healthComponent;

    private PlayerController _target;
    public HealthComponent Health => _healthComponent;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Rigidbody _bullet;
    [SerializeField] private float _minDamage = 5f;
    [SerializeField] private float _maxDamage = 15f;
    [SerializeField] private float _seeDistance = 2f;
    [SerializeField] private float _attackDistance = 2f;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _waypointChangeDistance = 1f;
    [SerializeField] private float _waypointStayTime = 3f;
    [SerializeField] private float _memorySize = 2;
    [SerializeField] private Waypoint[] _waypoints;
    
    private Waypoint _currentWaypoint;
    private Animator _animator;

    private float _nextAttackTime;
    
    private List<Waypoint> _memory = new List<Waypoint>();

    private void Start()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _waypoints = GameObject.FindObjectsOfType<Waypoint>();

    }

    private void FindTarget()
    {
        _target = FindObjectOfType<PlayerController>();
    }

    private void GetNextFreeWaypoint()
    {
        if (_currentWaypoint != null)
        {
            if(_memory.Count >= _memorySize) 
                _memory.RemoveAt(0);
            _currentWaypoint.Release();
        }

        _currentWaypoint = _waypoints.FirstOrDefault(wp => wp.IsFree && !_memory.Contains(wp));
        if (_currentWaypoint != null)
        {
            _currentWaypoint.Reserve(this);
            _memory.Add(_currentWaypoint);
        }
    }

    private void OnDrawGizmos()
    {
        if(_currentWaypoint != null)
            Gizmos.DrawLine(transform.position, _currentWaypoint.transform.position);
    }

    private void Update()
    {
        if (_target == null)
        {
            FindTarget();
            return;
        }

        if (_currentWaypoint == null)
            GetNextFreeWaypoint();

        var dirToPlayer = _target.transform.position - transform.position;
        var distToPlayer = dirToPlayer.magnitude;

        if (distToPlayer > _seeDistance && _currentWaypoint != null)
        {
            var currentWpDist = Vector3.Distance(transform.position, _currentWaypoint.transform.position);
            if (currentWpDist <= _waypointChangeDistance)
            {
                GetNextFreeWaypoint();
            }
            _navMesh.SetDestination(_currentWaypoint.transform.position);
            _navMesh.isStopped = false;
        }
        else if (distToPlayer < _seeDistance && distToPlayer > _attackDistance)
        {
            _navMesh.isStopped = false;
            _navMesh.SetDestination(_target.transform.position);
        }
        else if (distToPlayer < _attackDistance)
        {
            _navMesh.isStopped = true;
            var lookRot = Quaternion.LookRotation(dirToPlayer).eulerAngles;
            var targetRot = new Vector3(transform.eulerAngles.x, lookRot.y, transform.eulerAngles.z);
            transform.rotation = Quaternion.Euler(targetRot);
            Attack();
        }
    }

    private void Attack()
    {
        if (Time.time >= _nextAttackTime)
        {
            _animator.SetTrigger("Attack");
            var damage = Random.Range(_minDamage, _maxDamage);
            var bullet = Instantiate(_bullet, _shotPoint.position, _shotPoint.rotation);
            bullet.velocity = bullet.transform.forward * _bulletSpeed;
            var bulletComponent = bullet.GetComponent<Bullet>();
            if (bulletComponent != null)
            {
                bulletComponent.SetDamage(damage);
            }
            _nextAttackTime = Time.time + _attackDelay;
        }
          
        
    }
}
