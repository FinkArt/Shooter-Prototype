using System;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCaster : MonoBehaviour
{
    public static UpdateCaster Instance;
    
    public event Action OnUpdate;
    public event Action OnStart;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        OnStart?.Invoke();
    }

    private void Update()
    {
        OnUpdate?.Invoke();
    }
}

public class GameLogic
{
    public GameLogic()
    {
        var updateCaster = UpdateCaster.Instance;
        updateCaster.OnUpdate += OnUpdate;
        OnStart();
    }
    
    private void OnStart()
    {

    }

    private void OnUpdate()
    {

    }
}

public class MainClass : MonoBehaviour
{
    private List<ISystem> _systems = new List<ISystem>();
    //private SpawnConfig _spawnConfig;

    private void Awake()
    {
        var cameraSystem = new CameraSystem();
        var playerSystem = new PlayerSystem();
        var spawnSystem = new SpawnSystem(); //<- _spawnConfig
        _systems.Add(cameraSystem);
        _systems.Add(playerSystem);
        _systems.Add(spawnSystem);
    }

    private void Update()
    {
        foreach (var system in _systems)
        {
            system.UpdateSystem();
        }
    }
}

public interface ISystem
{
    void UpdateSystem();
}

public class CameraSystem : ISystem
{
    public void UpdateSystem()
    {
        
    }
}

public class PlayerSystem : ISystem
{
    public void UpdateSystem()
    {
    }
}

public class SpawnSystem : ISystem
{
    public void UpdateSystem()
    {
    }
}

