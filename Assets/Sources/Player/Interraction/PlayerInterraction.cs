using System;
using UnityEngine;

public class PlayerInterraction : MonoBehaviour
{   
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private float _interractionRadius = 1;
    [SerializeField] private LayerMask _mask;

    private readonly Collider[] _colliders = new Collider[4];

    private void Update()
    {
        if (_input.IsInterractionPressed)
        {
            TryInterract();
        }
    }

    private void TryInterract()
    {
        int length = Physics.OverlapSphereNonAlloc(transform.position, _interractionRadius, _colliders, _mask);

        for (int i = 0; i < length; i++)
        {
            if (_colliders[i].TryGetComponent(out IInterractable interractable))
            {
                interractable.Interract(_inventory);
            }
            else throw new Exception($"Объект \"{_colliders[i].name}\" находиться на слое \"Interractables\", но не реализует \"IInterractable\"!");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _interractionRadius);
    }
}
