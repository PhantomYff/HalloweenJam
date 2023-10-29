using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

using Random = UnityEngine.Random;

public class Reznya : MonoBehaviour
{
    [SerializeField] private float _secondsDelay;
    [SerializeField] private float _secondsDuration;
    [SerializeField] private float _additionalTime;

    [SerializeField] private Clip _music;
    [SerializeField] private Transform[] _monstersSpawnPositions;
    [SerializeField] private AnimationCurve _monstersSpawnDelay;
    [SerializeField] private ReznyaMonster[] _monstersPrefabs;
    [SerializeField] private Transform _player;

    private IAmbientAudio _ambientAudio;
    private IGameLoop _gameLoop;

    private readonly List<ReznyaMonster> _monsters = new();

    public IEnumerable<ReznyaMonster> Monsters => _monsters;

    [Inject]
    private void Construct(IAmbientAudio ambientAudio, IGameLoop gameLoop)
    {
        _ambientAudio = ambientAudio;
        _gameLoop = gameLoop;
    }

    /// <summary>
    /// Начать резню!
    /// </summary>
    public void NachatReznyu()
    {
        this.Coroutine(NachatReznyu_Internal());

        IEnumerator NachatReznyu_Internal()
        {
            _ambientAudio.SetClips(_music);

            yield return new WaitForSeconds(_secondsDelay);

            for (float t = 0f; t < _secondsDuration;)
            {
                float spawnDelay = _monstersSpawnDelay.Evaluate(t);
                SpawnRandomMonster();

                yield return new WaitForSeconds(spawnDelay);

                t += spawnDelay;
            }

            yield return new WaitForSeconds(_additionalTime);

            if (_monsters.Any())
                _gameLoop.Die();
        }
    }

    private void SpawnRandomMonster()
    {
        var spawnPosition = _monstersSpawnPositions[Random.Range(0, _monstersSpawnPositions.Length)];
        var prefab = _monstersPrefabs[Random.Range(0, _monstersPrefabs.Length)];

        var monster = Instantiate(prefab, spawnPosition.position, Quaternion.identity, null);
        _monsters.Add(monster);

        monster.Init(target: _player);
        monster.Died += OnMonsterDied;
    }

    private void OnMonsterDied(ReznyaMonster monster)
    {
        _monsters.Remove(monster);
    }
}
