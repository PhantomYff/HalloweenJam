using UnityEngine;

public class TestInterractable : MonoBehaviour, IInterractable
{
    public void Interract(PlayerInventory inventory)
    {
        Debug.Log("Тестовое взаимодействие!");
    }
}
