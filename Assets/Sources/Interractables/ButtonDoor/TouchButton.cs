using System;
using UnityEngine;

public class TouchButton : MonoBehaviour, IInterractable
{
    [SerializeField] Animator _animator;

    private const string PRESS_RELEASE_ANIMATION_NAME = "PressRelease";
    
    public event Action OnPressed;

    public void Interract(PlayerInventory inventory)
    {
        OnPressed?.Invoke();

        _animator.Play(PRESS_RELEASE_ANIMATION_NAME);
    }
}
