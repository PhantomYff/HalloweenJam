using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class AmbientAudio : MonoBehaviour, IAmbientAudio
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _fadeOutSeconds;
    [SerializeField] private Clip[] _clips;

    private IDisposable _playCoroutine;

    public void SetClips(Clip[] clips)
    {
        StartCoroutine(SetClips_Internal());

        IEnumerator SetClips_Internal()
        {
            _playCoroutine.Dispose();

            _audioSource.DOFade(0f, _fadeOutSeconds).SetLink(gameObject);
            yield return new WaitForSeconds(_fadeOutSeconds);

            _clips = clips;
            _playCoroutine = this.Coroutine(Play());
        }
    }

    private void Start()
    {
        _playCoroutine = this.Coroutine(Play());
    }

    private IEnumerator Play()
    {
        while (true)
        {
            foreach (Clip clip in _clips)
            {
                _audioSource.clip = clip.Audio;
                _audioSource.Play();

                yield return new WaitForSeconds(clip.Audio.length);
            }
        }
    }
}
