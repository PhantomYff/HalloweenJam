using UnityEngine;

public abstract class LyingItem : MonoBehaviour, IInterractable
{
    public void Interract(PlayerInventory inventory)
    {
        inventory.Item = GetItem();
    }

    protected abstract IItem GetItem();
}
