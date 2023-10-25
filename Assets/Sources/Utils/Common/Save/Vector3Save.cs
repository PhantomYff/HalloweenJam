using UnityEngine;

public sealed class Vector3Save : ISave<Vector3>
{
    private readonly string _key;

    private Vector3 _value;

    private const string XField = "_-x";
    private const string YField = "_-y";
    private const string ZField = "_-z";
    
    public Vector3Save(string key, Vector3 defaultValue = default)
    {
        _key = key + "_" + typeof(Vector3).FullName;

        _value = Load(defaultValue);
    }

    public Vector3 Value
    {
        get => _value;
        set
        {
            _value = value;

            PlayerPrefs.SetFloat(_key + XField, value.x);
            PlayerPrefs.SetFloat(_key + YField, value.y);
            PlayerPrefs.SetFloat(_key + ZField, value.z);
        }
    }

    private Vector3 Load(Vector3 defaultValue)
    {
        if (PlayerPrefs.HasKey(_key + XField) == false)
            return defaultValue;

        return new Vector3
        {
            x = PlayerPrefs.GetFloat(_key + XField, 0f),
            y = PlayerPrefs.GetFloat(_key + YField, 0f),
            z = PlayerPrefs.GetFloat(_key + ZField, 0f)
        };
    }
}
