using Gameplay.Player_Controller_Scripts.Weapon_Controller_Scripts;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private ConfigHolder _configHolder;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<KeyPressedSignal>();
        
        Container.BindInstance(_configHolder);
        Container.BindInterfacesAndSelfTo<InputManager>().AsSingle().NonLazy();
        
        //Container.Bind(typeof(ITickable)).To<InputManager>().AsSingle();
    }
}

public class KeyPressedSignal
{
    public KeyCode PressedButton;
}
