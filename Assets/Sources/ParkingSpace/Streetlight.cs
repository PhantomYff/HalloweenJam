using System;
using UnityEngine;
using Zenject;

public class Streetlight : MonoBehaviour
{
    public event Action<Streetlight> StateChanged;

    [SerializeField] private Light _light;

    [Header("Audio")]
    [SerializeField] private AudioSource _turningOffAudio;
    [SerializeField] private AudioSource _turningOnAudio;

    private IGameLoop _gameLoop;

    public bool IsEnabled { get; private set; }

    [Inject]
    private void Construct(IGameLoop gameLoop)
    {
        _gameLoop = gameLoop;
        _gameLoop.Restarted += OnRestart;
    }

    public void Enable()
    {
        _light.enabled = true;
        IsEnabled = true;

        StateChanged?.Invoke(this);
        _turningOnAudio.Play();
    }

    public void Disable()
    {
        _light.enabled = false;
        IsEnabled = false;

        StateChanged?.Invoke(this);
        _turningOffAudio.Play();
    }

    private void OnRestart()
    {
        _light.enabled = true;
        IsEnabled = true;

        StateChanged?.Invoke(this);
    }

    private void OnDestroy()
    {
        _gameLoop.Restarted -= OnRestart;
    }
}
