using UnityEngine;
using Zenject;

public class PCEventLauncher : MonoBehaviour
{
    [SerializeField] private PCEventRequest _request;

    private IParkingSpaceEvent _pcEvent;

    [Inject]
    private void Construct(IParkingSpaceEvent pcEvent)
    {
        _pcEvent = pcEvent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out _))
        {
            _pcEvent.LaunchEvent(_request);
        }
    }
}
