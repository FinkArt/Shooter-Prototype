using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private UIManager _uiManager;
    public override void InstallBindings()
    {
        Container.BindInstance(_uiManager);
    }
}