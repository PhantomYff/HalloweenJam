using System;
using UnityEngine;

[Serializable]
public class PCEventRequest
{
    [field: SerializeField] public float TurningOffSeconds { get; private set; }
    [field: SerializeField] public float TurningOnSeconds { get; private set; }

    [field: SerializeField] public float SecondsUntilDeath { get; private set; }
    [field: SerializeField] public float SecondsUntilContinuation { get; private set; }

    [field: SerializeField] public Streetlight StreetlightNotToFade { get; private set; }
}
