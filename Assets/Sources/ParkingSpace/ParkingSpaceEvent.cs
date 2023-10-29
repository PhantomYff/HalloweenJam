using System;
using System.Collections;
using UnityEngine;
using Zenject;

using Random = UnityEngine.Random;

public class ParkingSpaceEvent : MonoBehaviour, IParkingSpaceEvent
{
    [SerializeField] private PlayerIllumination _player;
    [SerializeField] private Streetlight[] _streetlights;

    private PCEventResult _result;
    private Streetlight _lightedStreetlight;

    private IGameLoop _gameLoop;

    public float DyingProgress { get; private set; }

    [Inject]
    private void Construct(IGameLoop gameLoop)
    {
        _gameLoop = gameLoop;
    }

    public void LaunchEvent(PCEventRequest request)
    {
        StartCoroutine(LaunchEvent_Internal());

        IEnumerator LaunchEvent_Internal()
        {
            yield return TurnOffStreetlights();
            yield return WaitForResult();

            switch (_result)
            {
                case PCEventResult.Death:
                    _gameLoop.Die();
                    DyingProgress = 0f;
                    break;
                case PCEventResult.Continuation:
                    yield return TurnOnStreetlights();
                    break;
                default:
                    throw new NotImplementedException();
            }

            IEnumerator TurnOffStreetlights()
            {
                _lightedStreetlight = request.StreetlightNotToFade;

                foreach (Streetlight streetlight in _streetlights)
                {
                    if (streetlight.GetInstanceID() == _lightedStreetlight.GetInstanceID())
                        continue;

                    this.InvokeDelayed(streetlight.Disable, secondsDelay: Random.Range(0f, request.TurningOffSeconds));
                }

                yield return new WaitForSeconds(request.TurningOffSeconds);
            }

            IEnumerator WaitForResult()
            {
                float secondsUntilDeath = 0f, secondsUntilContinuation = 0f;

                while (
                    secondsUntilDeath < request.SecondsUntilDeath &&
                    secondsUntilContinuation < request.SecondsUntilContinuation)
                {
                    secondsUntilDeath += Time.deltaTime;

                    if (_player.IsIlluminated)
                    {
                        secondsUntilContinuation += Time.deltaTime;
                        secondsUntilDeath = 0f;
                    }

                    DyingProgress = Mathf.Lerp(0f, request.SecondsUntilDeath, secondsUntilDeath);

                    yield return null;
                }

                _result = secondsUntilContinuation >= request.SecondsUntilContinuation
                    ? PCEventResult.Continuation
                    : PCEventResult.Death;
            }

            IEnumerator TurnOnStreetlights()
            {
                foreach (Streetlight streetlight in _streetlights)
                {
                    if (streetlight.GetInstanceID() == _lightedStreetlight.GetInstanceID())
                        continue;

                    this.InvokeDelayed(streetlight.Enable, Random.Range(0f, request.TurningOnSeconds));
                }
                yield return new WaitForSeconds(request.TurningOnSeconds);
            }
        }
    }
}
