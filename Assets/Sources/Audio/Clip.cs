using System;
using UnityEngine;

[Serializable]
public class Clip
{
    [field: SerializeField] public AudioClip Audio { get; private set; }
    [field: SerializeField, Range(0f, 1f)] public float Volume { get; private set; }
}
