using UnityEngine;

/// <summary>
/// Это будет подбираться игроком для активации резни!
/// </summary>
public class LyingDrobovik : MonoBehaviour, IInterractable
{
    [SerializeField] private Drobovik _drobovik;

    public void Interract(PlayerInventory inventory)
    {
        _drobovik.PickUp();
    }
}
