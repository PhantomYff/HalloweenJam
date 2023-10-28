using UnityEngine;
using Zenject;

public class MainSceneBootstrap : MonoInstaller
{
    [SerializeField] private DeathData _deathData;
    [SerializeField] private Interface<IGameLoop> _gameLoop;
    [SerializeField] private Interface<IParkingSpaceEvent> _psEvent;

    public override void InstallBindings()
    {
        RegisterStaticData();

        RegisterGameLoop();
        RegisterParkingSpaceEvent();
    }

    private void RegisterStaticData()
    {
        Container.Bind<DeathData>().FromInstance(_deathData).AsSingle();
    }

    private void RegisterGameLoop()
    {
        Container.Bind<IGameLoop>().FromInstance(_gameLoop.Value).AsSingle();
    }

    private void RegisterParkingSpaceEvent()
    {
        Container.Bind<IParkingSpaceEvent>().FromInstance(_psEvent.Value).AsSingle();
    }
}
