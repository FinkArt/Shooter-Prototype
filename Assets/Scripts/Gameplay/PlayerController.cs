using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    private NavMeshAgent _navMesh;
    
    // [SerializeField]
    // private Transform _moveToPoint;

    [SerializeField] private HealthComponent _healthComponent;

    public HealthComponent Health => _healthComponent;

    public static event Action<PlayerController> OnPlayerSpawned;

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        OnPlayerSpawned?.Invoke(this);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Input.mousePosition;
            var ray = _cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity))
            {
                _navMesh.SetDestination(hitInfo.point);   
            }
        }
    }
}
