using System;

public interface IGameLoop
{
    event Action Restarted;
    event Action PlayerDied;

    void Die();
}
