using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

public class ScreenFade : MonoBehaviour
{
    [SerializeField] private Image _screen;
    [SerializeField] private float _fadeOutSeconds;

    private IGameLoop _gameLoop;
    private DeathData _deathData;

    [Inject]
    private void Construct(IGameLoop gameLoop, DeathData deathData)
    {
        _gameLoop = gameLoop;
        _deathData = deathData;

        _gameLoop.PlayerDied += OnPlayerDied;
        _gameLoop.Restarted += Restarted;
    }

    private void OnPlayerDied()
    {
        FadeScreen(1f, _deathData.SecondsUntilRestart);
    }

    private void Restarted()
    {
        FadeScreen(0f, _fadeOutSeconds);
    }

    private void FadeScreen(float to, float seconds)
    {
        _screen.DOFade(to, seconds).SetLink(gameObject);
    }

    private void OnDestroy()
    {
        _gameLoop.PlayerDied -= OnPlayerDied;
        _gameLoop.Restarted -= Restarted;
    }
}
