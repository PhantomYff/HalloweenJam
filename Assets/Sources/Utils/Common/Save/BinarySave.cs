using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinarySave<T> : ISave<T>
{
    private static readonly BinaryFormatter s_binaryFormatter;

    private readonly string _key;

    private T _value;

    static BinarySave()
    {
        s_binaryFormatter = new();
    }

    public BinarySave(string key, T defaultValue)
    {
        _key = key + "_-binary_" + typeof(T).FullName;

        _value = Load(defaultValue);
    }

    public T Value
    {
        get => _value;
        set
        {
            _value = value;

            using (var stream = new MemoryStream())
            {
                s_binaryFormatter.Serialize(stream, value);
                stream.Flush();
                stream.Position = 0;
                string serializedObject = Convert.ToBase64String(stream.ToArray());

                PlayerPrefs.SetString(_key, serializedObject);
            }
        }
    }

    private T Load(T defaultValue)
    {
        if (PlayerPrefs.HasKey(_key) == false)
        {
            return defaultValue;
        }

        string serializedObject = PlayerPrefs.GetString(_key);

        byte[] buffer = Convert.FromBase64String(serializedObject);
        using (var stream = new MemoryStream(buffer))
        {
            stream.Seek(0, SeekOrigin.Begin);
            return (T)s_binaryFormatter.Deserialize(stream);
        }
    }
}
