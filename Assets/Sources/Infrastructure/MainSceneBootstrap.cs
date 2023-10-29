using System;
using UnityEngine;
using Zenject;

public class MainSceneBootstrap : MonoInstaller
{
    [SerializeField] private DeathData _deathData;
    [SerializeField] private Interface<IGameLoop> _gameLoop;
    [SerializeField] private Interface<IParkingSpaceEvent> _psEvent;
    [SerializeField] private Interface<IAmbientAudio> _ambientAudio;
    [SerializeField] private Interface<ITextDisplay> _textDisplay;

    public override void InstallBindings()
    {
        RegisterStaticData();

        RegisterGameLoop();
        RegisterParkingSpaceEvent();
        RegisterAmbientAudio();
        RegisterTextDisplay();
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

    private void RegisterAmbientAudio()
    {
        Container.Bind<IAmbientAudio>().FromInstance(_ambientAudio.Value).AsSingle();
    }

    private void RegisterTextDisplay()
    {
        Container.Bind<ITextDisplay>().FromInstance(_textDisplay.Value).AsSingle();
    }
}
