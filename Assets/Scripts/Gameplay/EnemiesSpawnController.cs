using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesSpawnController : MonoBehaviour
{
    [SerializeField] private EnemyController _enemy;
    [SerializeField] private Transform[] _spawnPoint;
    
    private List<EnemyController> _enemies = new List<EnemyController>();
    
    [SerializeField] private int _numbersOfEnemies = 10;
    [SerializeField] private float _spawnDelay = 2f;

    private float _nextSpawnTime;

    public event Action<EnemyController> OnEnemySpawned;
    public event Action<EnemyController> OnEnemyDespawned;

    private void Update()
    {
        if (_enemies.Count < _numbersOfEnemies)
        {
            if (Time.time >= _nextSpawnTime)
            {
                SpawnEnemy();
                _nextSpawnTime = Time.time + _spawnDelay;
            }
        }
    }

    private void SpawnEnemy()
    {
        var spawnPoint = Random.Range(0, _spawnPoint.Length);
        var enemy = Instantiate(_enemy, _spawnPoint[spawnPoint].transform.position, Quaternion.identity);
        _enemies.Add(enemy);
        OnEnemySpawned?.Invoke(enemy);
        enemy.Health.OnDead += () => { DeadEnemy(enemy); };
        Debug.Log(_enemies.Count);
    }

    private void DeadEnemy(EnemyController enemy)
    {
        _enemies.Remove(enemy);
        OnEnemyDespawned?.Invoke(enemy);
    }
}
