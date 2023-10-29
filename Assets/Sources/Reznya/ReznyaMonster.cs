using System;
using UnityEngine;

public class ReznyaMonster : MonoBehaviour
{
    public event Action<ReznyaMonster> Died;

    [SerializeField] private float _speed;
    [SerializeField] private GameObject _onKillPrefab;
    [SerializeField] private float _onKillPrefabDuration;

    private Transform _target;

    public void Init(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        Vector3 direction = _target.transform.position - transform.position;

        direction.y = 0f;
        direction.Normalize();

        transform.position += _speed * Time.deltaTime * direction;
    }

    public void Kill()
    {
        Destroy(_onKillPrefab, _onKillPrefabDuration);
        Died.Invoke(this);
    }
}
