using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] private HealthBarView _healthBarPrefab;
    [SerializeField] private RectTransform _healthBarContainer;
    [SerializeField] private Camera _camera;
    [SerializeField] private HealthConfig _healthConfig;
    
    private Dictionary<HealthComponent, HealthBarView> _healthBarViews = new Dictionary<HealthComponent, HealthBarView>();
    
    private void Awake()
    {
        PlayerController.OnPlayerSpawned += PlayerControllerOnOnPlayerSpawned;
        EnemyController.OnBotSpawned += EnemyControllerOnOnBotSpawned;
    }

    private void EnemyControllerOnOnBotSpawned(EnemyController enemy)
    {
        SpawnHealthBar(enemy.Health);
    }

    private void PlayerControllerOnOnPlayerSpawned(PlayerController player)
    {
        SpawnHealthBar(player.Health);
    }

    private void Update()
    {
        foreach (var healthBarView in _healthBarViews)
        {
            var worldPlayerPos = healthBarView.Key.transform.position;
            var uiPos = _camera.WorldToScreenPoint(worldPlayerPos);
            healthBarView.Value.SetPosition(uiPos + new Vector3(0f, _healthConfig.HealthBarOffsetY, 0f)); 
        }
    }

    private void SpawnHealthBar(HealthComponent healthComponent)
    {
        var hpBar = Instantiate(_healthBarPrefab, _healthBarContainer);
        _healthBarViews.Add(healthComponent, hpBar);
        healthComponent.OnHealthChanged += (hp, maxHp) => OnHealthChanged(healthComponent, hp, maxHp);
    }

    private void OnHealthChanged(HealthComponent healthComponent, float health, float maxHealth)
    {
        var hpBar = _healthBarViews[healthComponent];
        hpBar.SetHealth(health / maxHealth);
    }
}
