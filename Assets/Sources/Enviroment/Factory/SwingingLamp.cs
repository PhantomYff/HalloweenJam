using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SwingingLamp : MonoBehaviour
{
    [SerializeField] private AnimationCurve _swingCurve;
    [SerializeField] private AnimationCurve _blinkingCurve;
    [SerializeField] private CollisionEvents _triggerEvents;
    [SerializeField] private Light _light;

    
    private void Start()
    {
        StartCoroutine(SwingCorutine());

        _triggerEvents.OnTriggerEntered += StartBlinking;
    }

    private void StartBlinking(Collider _collider)
    {
        StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator SwingCorutine()
    {
        float elapsedTime = 0;
        while (true)
        {
            transform.rotation = Quaternion.Euler(_swingCurve.Evaluate(elapsedTime), -90, -90);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
    
    private IEnumerator BlinkCoroutine()
    {
        float elapsedTime = 0;
        float maxTime = _blinkingCurve.keys[^1].time; // https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/ranges-indexes
        while (true)
        {
            _light.intensity = _blinkingCurve.Evaluate(elapsedTime);

            if (elapsedTime >= maxTime) break;
            
            yield return null;
        }

        _light.intensity = 0;
    }
}

