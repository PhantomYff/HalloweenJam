using System;
using UnityEngine;
using Zenject;

public class PCEventLauncher : MonoBehaviour
{
    [SerializeField] private PCEventRequest _request;

    private IParkingSpaceEvent _pcEvent;
    private IGameLoop _gameLoop;

    [Inject]
    private void Construct(IParkingSpaceEvent pcEvent, IGameLoop gameLoop)
    {
        _pcEvent = pcEvent;
        _gameLoop = gameLoop;

        _gameLoop.Restarted += OnRestart;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out _))
        {
            _pcEvent.LaunchEvent(_request);
            gameObject.SetActive(false);
        }
    }

    private void OnRestart()
    {
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        _gameLoop.Restarted -= OnRestart;
    }
}
