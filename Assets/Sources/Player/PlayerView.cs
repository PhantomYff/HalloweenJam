using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField, Range(0f, 1f)] private float _movingAnimationThreshold;

    private readonly int _isMovingFieldID = Animator.StringToHash("IsMoving");

    private void LateUpdate()
    {
        bool isMoving = Mathf.Abs(_input.Direction.magnitude) > _movingAnimationThreshold;
        _animator.SetBool(_isMovingFieldID, isMoving);

        if (_input.Direction.x != 0f)
            _sprite.flipX = _input.Direction.x < 0f;
    }
}
