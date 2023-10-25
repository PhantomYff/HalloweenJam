using System;
using System.Reflection;
using System.ComponentModel;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class UtilsExtensions
{
    public static Type GetFieldType(this FieldInfo field)
    {
        return field.FieldType.IsArray
            ? field.FieldType.GetElementType()
            : field.FieldType;
    }
}
