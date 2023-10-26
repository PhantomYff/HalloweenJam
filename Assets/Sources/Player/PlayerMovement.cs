using System;
using System.Linq;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private float _speed;
    [SerializeField] private float _coyoteTime;

    [Header("Jump Settings")]
    [SerializeField] private float _jumpStrength;
    [SerializeField] private float _maxJumpContinuation;
    [SerializeField] private float _jumpContinuationStrength;

    [Header("Ground Check")]
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private LayerMask _groundCheckLayers;
    [SerializeField] private Transform[] _groundCheckPoints;

    private bool _isOnGround;
    private bool _toContinueJump;

    private IDisposable _coyoteTimeCoroutine;

    private void Update()
    {
        if (_isOnGround && _input.ToJump)
        {
            _rigidbody.SetVelocityY(_jumpStrength);
            _isOnGround = false;

            this.Coroutine(ContinueJump());
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (CheckForGround())
        {
            _coyoteTimeCoroutine?.Dispose();
            _coyoteTimeCoroutine = this.Coroutine(CoyoteTime());
        }

        if (_toContinueJump)
        {
            _rigidbody.AddForce(Time.fixedDeltaTime * _jumpContinuationStrength * Vector3.up);
        }

        void Move()
        {
            Vector2 movement = _speed * Time.fixedDeltaTime * _input.Direction;
            _rigidbody.SetVelocityX(movement.x);
            _rigidbody.SetVelocityZ(movement.y);
        }
    }

    private IEnumerator CoyoteTime()
    {
        _isOnGround = true;
        yield return new WaitForSeconds(_coyoteTime);
        _isOnGround = false;
    }

    private IEnumerator ContinueJump()
    {
        _toContinueJump = true;

        for (
            float t = 0f;
                _input.IsJumpHolding &&
                _isOnGround == false &&
                t < _maxJumpContinuation;
            t += Time.deltaTime)
        {
            yield return null;
        }

        _toContinueJump = false;
    }

    private bool CheckForGround()
    {
        return _groundCheckPoints
            .Any(x => Physics.Raycast(x.position, Vector3.down, _groundCheckDistance, _groundCheckLayers));
    }

    private void OnDrawGizmos()
    {
        foreach (Transform point in _groundCheckPoints)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(point.position, point.position + Vector3.down * _groundCheckDistance);
        }
    }
}
