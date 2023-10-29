using DG.Tweening;
using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class Extensions
{
    public static void SetVelocityX(this Rigidbody rigidbody, float x)
    {
        var velocity = rigidbody.velocity;
        velocity.x = x;
        rigidbody.velocity = velocity;
    }

    public static void SetVelocityY(this Rigidbody rigidbody, float y)
    {
        var velocity = rigidbody.velocity;
        velocity.y = y;
        rigidbody.velocity = velocity;
    }

    public static void SetVelocityZ(this Rigidbody rigidbody, float z)
    {
        var velocity = rigidbody.velocity;
        velocity.z = z;
        rigidbody.velocity = velocity;
    }

    public static IDisposable Coroutine(this MonoBehaviour monoBehaviour, IEnumerator routine)
    {
        Coroutine coroutine = monoBehaviour.StartCoroutine(routine);
        return new DelegateDisposableAdapter(() => monoBehaviour.StopCoroutine(coroutine));
    }

    public static IDisposable InvokeDelayed(this MonoBehaviour monoBehaviour, Action action, float secondsDelay)
    {
        return monoBehaviour.Coroutine(InvokeDelayed_Internal());

        IEnumerator InvokeDelayed_Internal()
        {
            yield return new WaitForSeconds(secondsDelay);
            action.Invoke();
        }
    }

    public static Vector3 YToZ(this Vector2 origin)
    {
        return new Vector3(origin.x, 0f, origin.y);
    }
}
