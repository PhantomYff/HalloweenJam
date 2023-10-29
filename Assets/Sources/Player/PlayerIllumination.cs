using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIllumination : MonoBehaviour
{
    private readonly LinkedList<Streetlight> _all = new();
    private readonly LinkedList<Streetlight> _illuminating = new();

    public bool IsIlluminated => _all.Any();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Streetlight streetlight))
        {
            _all.AddLast(streetlight);
            streetlight.StateChanged += OnStateChanged;

            if (streetlight.IsEnabled)
                _illuminating.AddLast(streetlight);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Streetlight streetlight))
        {
            _all.Remove(streetlight);
            streetlight.StateChanged -= OnStateChanged;

            if (streetlight.IsEnabled)
                _illuminating.Remove(streetlight);
        }
    }

    private void OnStateChanged(Streetlight sender)
    {
        if (sender.IsEnabled)
            _illuminating.AddLast(sender);
        else
            _illuminating.Remove(sender);
    }

    private void OnDestroy()
    {
        foreach (Streetlight streetlight in _all)
        {
            streetlight.StateChanged -= OnStateChanged;
        }
    }
}
