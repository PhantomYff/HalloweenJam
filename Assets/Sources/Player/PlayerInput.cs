using System;
using System.Linq;
using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private KeyCode[] _jumpKeys;
    [SerializeField] private float _jumpBufferSeconds;

    private IDisposable _jumpBufferCoroutine;

    public Vector2 Direction { get; private set; }
    public bool ToJump { get; private set; }
    public bool IsJumpHolding { get; private set; }

    private void Update()
    {
        Direction = Vector2.ClampMagnitude(new Vector2
        {
            x = Input.GetAxis(Axis.Horizontal),
            y = Input.GetAxis(Axis.Vertical)
        }, 1f);

        IsJumpHolding = _jumpKeys.Any(x => Input.GetKey(x));
        bool jumpPressed = _jumpKeys.Any(x => Input.GetKeyDown(x));

        if (jumpPressed)
        {
            _jumpBufferCoroutine?.Dispose();
            _jumpBufferCoroutine = this.Coroutine(JumpBuffer());
        }
    }

    private IEnumerator JumpBuffer()
    {
        ToJump = true;
        yield return new WaitForSeconds(_jumpBufferSeconds);
        ToJump = false;
    }
}
