using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Zenject;

public class AnimatedParameter : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _modifier = 1f;
    [SerializeField] private UnityEvent<float> _animated;
    [SerializeField] private bool _restartOnRestart;

    private IGameLoop _gameLoop;

    [Inject]
    private void Construct(IGameLoop gameLoop)
    {
        _gameLoop = gameLoop;
        _gameLoop.Restarted += OnRestart;
    }

    private IEnumerator Start()
    {
        for (float t = 0f; t < _animationCurve.keys[^1].time; t += Time.deltaTime * _speed)
        {
            _animated.Invoke(_animationCurve.Evaluate(t) * _modifier);
            yield return null;
        }
    }

    private void OnRestart()
    {
        if (_restartOnRestart)
            StartCoroutine(Start());
    }

    private void OnDestroy()
    {
        _gameLoop.Restarted -= OnRestart;
    }
}
