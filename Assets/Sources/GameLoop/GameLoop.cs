using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class GameLoop : MonoBehaviour, IGameLoop
{
    public event Action Restarted;
    public event Action PlayerDied;

    private DeathData _deathData;
    private bool _playerDied;

    [Inject]
    private void Construct(DeathData deathData)
    {
        _deathData = deathData;
    }

    public void Die()
    {
        if (_playerDied)
            throw new InvalidOperationException();

        StartCoroutine(Die_Internal());

        IEnumerator Die_Internal()
        {
            _playerDied = true;
            PlayerDied?.Invoke();

            yield return new WaitForSecondsRealtime(_deathData.SecondsUntilRestart);

            Restarted?.Invoke();
            _playerDied = false;
        }
    }
}
