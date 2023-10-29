using UnityEngine;

public class PlayerRespawn : MonoBehaviour, IPlayerRespawn
{
    private readonly ISave<Vector3> _respawnPoint = CreateSave.Vector3("RespawnPoint");

    public void SetRespawnPoint(Vector3 newPoint)
    {
        _respawnPoint.Value = newPoint;
    }
}
