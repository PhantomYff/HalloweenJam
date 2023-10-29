using System.Collections;
using UnityEngine;

public class Drobovik : MonoBehaviour
{
    [SerializeField] private Reznya _reznya;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private AudioSource _shootAudio;
    [SerializeField] private ParticleSystem _shootParticles;

    [Header("Settings")]
    [SerializeField] private float _defeatAngle;
    [SerializeField] private float _reloadDuration;

    private bool _isReady = true;

    public void PickUp()
    {
        gameObject.SetActive(true);

        _reznya.NachatReznyu();
    }

    private void Update()
    {
        if (_input.IsShootButtonPressed && _isReady)
        {
            this.Coroutine(Reload());
            Shoot();
        }
    }

    private void Shoot()
    {
        foreach (var monster in _reznya.Monsters)
        {
            var delta = monster.transform.position - transform.position;
            print(Vector3.Angle(transform.right, delta));

            if (Vector3.Angle(transform.right, delta) < _defeatAngle)
            {
                monster.Kill();
            }
        }

        _shootAudio?.Play();
        _shootParticles?.Play();
    }

    private IEnumerator Reload()
    {
        _isReady = false;
        yield return new WaitForSeconds(_reloadDuration);
        _isReady = true;
    }
}
