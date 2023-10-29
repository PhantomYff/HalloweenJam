using UnityEngine;

public class ButtonDoor : MonoBehaviour
{   
    [SerializeField] private TouchButton _button;
    [SerializeField] private Animator _animator;

    private const string OPEN_ANIMATION_NAME = "Open";
    private const string CLOSE_ANIMATION_NAME = "Close";

    private bool isOpen;

    private void Start()
    {
        _button.OnPressed += OpenClose;
        
    }

    private void OpenClose()
    {
        if (isOpen) _animator.Play(OPEN_ANIMATION_NAME);
        else _animator.Play(CLOSE_ANIMATION_NAME);
    }
}
