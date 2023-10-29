using System;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;

[EditorBrowsable(EditorBrowsableState.Never)]
internal static class UtilsExtensions
{
    public static Type GetFieldType(this FieldInfo field)
    {
        return field.FieldType.IsArray
            ? field.FieldType.GetElementType()
            : field.FieldType;
    }

    public static TValue Cache<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> source)
    {
        if (dictionary.TryGetValue(key, out TValue value))
            return value;

        TValue result = source(key);
        dictionary[key] = result;
        return result;
    }
}
