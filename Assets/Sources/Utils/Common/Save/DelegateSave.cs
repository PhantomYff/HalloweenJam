using System;

public sealed class DelegateSave<T> : ISave<T>
{
    private readonly string _key;
    private readonly Action<string, T> _save;

    private T _value;

    public DelegateSave(string key, Action<string, T> save, Func<string, T> load)
    {
        _key = key + "_" + typeof(T).FullName;
        _save = save;

        _value = load.Invoke(_key);
    }

    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            _save.Invoke(_key, value);
        }
    }
}
