using System;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionEvents : MonoBehaviour
{
    public event Action<Collision> OnCollisionEntered;
    public event Action<Collision> OnCollisionStayed;
    public event Action<Collision> OnCollisionExited;
    public event Action<Collider> OnTriggerEntered;
    public event Action<Collider> OnTriggerStayed;
    public event Action<Collider> OnTriggerExited;

    public void OnCollisionEnter(Collision collision)
    {
        OnCollisionEntered?.Invoke(collision);
    }
    
    public void OnCollisionStay(Collision collision)
    {
        OnCollisionStayed?.Invoke(collision);
    }
    
    public void OnCollisionExit(Collision collision)
    {
        OnCollisionExited?.Invoke(collision);
    }
    
    public void OnTriggerEnter(Collider collider)
    {
        OnTriggerEntered?.Invoke(collider);
    }
    
    public void OnTriggerStay(Collider collider)
    {
        OnTriggerStayed?.Invoke(collider);
    }

    public void OnTriggerExit(Collider collider)
    {
        OnTriggerExited?.Invoke(collider);
    }
}
