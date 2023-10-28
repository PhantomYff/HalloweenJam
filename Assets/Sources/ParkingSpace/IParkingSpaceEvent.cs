public interface IParkingSpaceEvent
{
    void LaunchEvent(PCEventRequest request);

    float DyingProgress { get; }
}
