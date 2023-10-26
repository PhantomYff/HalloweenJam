using System;
using UnityEngine;

public class PlayerInterraction : MonoBehaviour
{   
    [SerializeField] private PlayerInput _input;
    [SerializeField] private float _interractionRadius = 1;
    [SerializeField] private LayerMask _mask;
    private Collider[] _colliders;

    private void Update()
    {
        UpdateColliders();
        if (_input.IsInterractionPressed)
        {
            TryInterract();
        }
    }

    private void TryInterract()
    {
        if (_colliders.Length == 0) return;

        if (_colliders[0].TryGetComponent<IInterractable>(out IInterractable interractable))
        {
            interractable.Interract();
        }
        else throw new Exception("Все обьекты на слое \"Interractables\" должны реализовывать \"IInterractable\"!");
    }
    
    private void UpdateColliders()
    {
        _colliders = Physics.OverlapSphere(transform.position, _interractionRadius, _mask); 

    }    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _interractionRadius);
    }
}
