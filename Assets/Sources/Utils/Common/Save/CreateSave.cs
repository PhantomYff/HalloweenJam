using UnityEngine;

public static partial class CreateSave
{
    public static ISave<int> Int(string key, int defaultValue = 0) => new DelegateSave<int>(key,
        (k, x) => PlayerPrefs.SetInt(k, x),
        (k) => PlayerPrefs.GetInt(k, defaultValue));

    public static ISave<float> Float(string key, float defaultValue = 0f) => new DelegateSave<float>(key,
        (k, x) => PlayerPrefs.SetFloat(k, x),
        (k) => PlayerPrefs.GetFloat(k, defaultValue));

    public static ISave<string> String(string key, string defaultValue = "") => new DelegateSave<string>(key,
        (k, x) => PlayerPrefs.SetString(k, x),
        (k) => PlayerPrefs.GetString(k, defaultValue));

    public static ISave<bool> Bool(string key, bool defaultValue = false) => new DelegateSave<bool>(key,
        (k, x) => PlayerPrefs.SetString(k, x ? "Y" : "N"),
        (k) => PlayerPrefs.GetString(k, defaultValue ? "Y" : "N") == "Y");

    public static ISave<Vector3> Vector3(string key, Vector3 defaultValue = default)
        => new Vector3Save(key, defaultValue);

    public static ISave<T> Binary<T>(string key, T defaultValue = default)
        => new BinarySave<T>(key, defaultValue);
}
