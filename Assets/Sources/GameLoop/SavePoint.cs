using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public class SavePoint : MonoBehaviour
{
    private IPlayerRespawn _playerRespawn;

    [Inject]
    private void Construct(IPlayerRespawn playerRespawn)
    {
        _playerRespawn = playerRespawn;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out _))
        {
            _playerRespawn.SetRespawnPoint(transform.position);
        }
    }
}
