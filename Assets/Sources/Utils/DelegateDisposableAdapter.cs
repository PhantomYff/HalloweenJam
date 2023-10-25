using System;

public class DelegateDisposableAdapter : IDisposable
{
    private readonly Action _dispose;

    public DelegateDisposableAdapter(Action dispose)
	{
        _dispose = dispose;
    }

    public void Dispose()
    {
        _dispose.Invoke();
    }
}
