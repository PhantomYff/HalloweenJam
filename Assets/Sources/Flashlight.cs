using UnityEngine;
using DG.Tweening;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;

    [Header("Rotating")]
    [SerializeField] private float _rotatingDuration;
    [SerializeField] private Ease _rotatingEase = DOTween.defaultEaseType;

    private Tween _rotatingTween;

    private void LateUpdate()
    {
        const float AngleAdjustment = -90f;

        if (_input.Direction == Vector2.zero)
            return;

        Vector3 rotation = Quaternion.LookRotation(_input.Direction.YToZ(), upwards: Vector3.up).eulerAngles;
        rotation.y += AngleAdjustment;

        _rotatingTween?.Kill();
        _rotatingTween = transform
            .DORotate(rotation, _rotatingDuration)
            .SetEase(_rotatingEase)
            .SetLink(gameObject);
    }
}
