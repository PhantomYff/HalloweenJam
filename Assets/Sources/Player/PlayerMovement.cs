using System;
using System.Linq;
using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    public event Action Jumped;
    public event Action Landed;

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

    private IGameLoop _gameLoop;
    private ISave<Vector3> _respawnPoint;

    [Inject]
    private void Construct(IGameLoop gameLoop)
    {
        _gameLoop = gameLoop;
        _gameLoop.Restarted += OnRestart;

        _respawnPoint = CreateSave.Vector3("RespawnPoint", transform.position);
    }

    public void SetRespawnPoint(Vector3 point)
    {
        _respawnPoint.Value = point;
    }

    private void Update()
    {
        if (_isOnGround && _input.ToJump)
        {
            _rigidbody.SetVelocityY(_jumpStrength);
            _isOnGround = false;

            Jumped?.Invoke();
            this.Coroutine(ContinueJump());
        }
    }

    private void FixedUpdate()
    {
        Move();

        bool isOnGroundNow = CheckForGround();

        if (isOnGroundNow && _isOnGround == false)
        {
            Landed?.Invoke();
        }

        if (isOnGroundNow)
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

    private void OnRestart()
    {
        transform.position = _respawnPoint.Value;
    }

    private void OnDestroy()
    {
        _gameLoop.Restarted -= OnRestart;
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
