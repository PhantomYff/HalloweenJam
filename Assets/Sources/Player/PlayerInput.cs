using System;
using System.Linq;
using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private KeyCode[] _jumpKeys;
    [SerializeField] private KeyCode[] _interractionKeys;
    [SerializeField] private float _jumpBufferSeconds;

    private IDisposable _jumpBufferCoroutine;

    public Vector2 Direction { get; private set; }
    public bool ToJump { get; private set; }
    public bool IsJumpHolding { get; private set; }
    public bool IsInterractionPressed { get; private set; }


    private void Update()
    {
        CalculateDirection();
        CalculateJump();
        CalculateInterractions();
    }

    private void CalculateDirection()
    {
        Direction = Vector2.ClampMagnitude(new Vector2
        {
            x = Input.GetAxis(Axis.Horizontal),
            y = Input.GetAxis(Axis.Vertical)
        }, 1f);
    }

    private void CalculateJump()
    {
        IsJumpHolding = _jumpKeys.Any(x => Input.GetKey(x));

        bool jumpPressed = _jumpKeys.Any(x => Input.GetKeyDown(x));

        if (jumpPressed)
        {
            _jumpBufferCoroutine?.Dispose();
            _jumpBufferCoroutine = this.Coroutine(JumpBuffer());
        }
    }

    private void CalculateInterractions()
    {
        bool interractionPressed = _interractionKeys.Any(x => Input.GetKeyDown(x));
        
        if (interractionPressed)
        {
            IsInterractionPressed = true;
        }
        else
        {
            IsInterractionPressed = false;
        }
    }

    private IEnumerator JumpBuffer()
    {
        ToJump = true;
        yield return new WaitForSeconds(_jumpBufferSeconds);
        ToJump = false;
    }
}
