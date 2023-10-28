using System;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField, Range(0f, 1f)] private float _movingAnimationThreshold;

    [Header("Audio")]
    [SerializeField] private AudioSource _jumpSound;
    [SerializeField] private AudioSource _landSound;

    private readonly int _isMovingFieldID = Animator.StringToHash("IsMoving");

    private void Start()
    {
        _movement.Jumped += OnJump;
        _movement.Landed += OnLand;
    }

    private void LateUpdate()
    {
        bool isMoving = Mathf.Abs(_input.Direction.magnitude) > _movingAnimationThreshold;
        _animator.SetBool(_isMovingFieldID, isMoving);

        if (_input.Direction.x != 0f)
            _sprite.flipX = _input.Direction.x < 0f;
    }

    private void OnJump()
    {
        _jumpSound.Play();
    }

    private void OnLand()
    {
        _landSound.Play();
    }

    private void OnDestroy()
    {
        _movement.Jumped -= OnJump;
        _movement.Landed -= OnLand;
    }
}
