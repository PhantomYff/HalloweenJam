using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInterractable : MonoBehaviour, IInterractable
{
    public void Interract()
    {
        Debug.Log("Тестовое взаимодействие!");
    }
}
