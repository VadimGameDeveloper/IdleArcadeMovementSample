using UnityEngine;
using Zenject;

public class GameInstancesInstaller : MonoInstaller
{
    public Camera MainCamera;
    public Joystick MainJoystick;
    public override void InstallBindings()
    {
        Container.BindInstance(MainCamera).AsSingle();
        Container.BindInstance(MainJoystick).AsSingle();
    }
}