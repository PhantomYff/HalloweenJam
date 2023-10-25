using UnityEngine;

[System.Serializable]
public class Interface<T>
{
    [SerializeField] private Object _value;

    public T Value => this;

    public static implicit operator T(Interface<T> instance)
    {
        if (instance._value is T result)
            return result;

        // This point isn't reachable.
        throw new System.NotImplementedException();
    }
}
