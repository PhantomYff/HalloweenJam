#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.ComponentModel;

[EditorBrowsable(EditorBrowsableState.Never)]
[CustomPropertyDrawer(typeof(ResourceAsset<>))]
public class ResourceAssetDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var pathProperty = property.FindPropertyRelative("_assetPath");
        var resourceType = fieldInfo.GetFieldType().GetGenericArguments()[0];

        var asset = pathProperty.stringValue != string.Empty
            ? Resources.Load(pathProperty.stringValue)
            : null;

        label.text += $" ({resourceType.Name})";

        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.BeginChangeCheck();

        asset = EditorGUI.ObjectField(position, label, asset, typeof(Object), allowSceneObjects: false);

        if (EditorGUI.EndChangeCheck())
        {
            pathProperty.stringValue = ValidateAsset(asset, resourceType);
        }

        EditorGUI.EndProperty();
    }

    private static string ValidateAsset(Object asset, System.Type expectedType)
    {
        const int ASSETS_RESOURCES_FOLDER_LENGTH = 17;

        if (asset == null)
        {
            return string.Empty;
        }

        string path = AssetDatabase.GetAssetPath(asset);
        path = RemoveExtension(path);

        if (path.StartsWith(@"Assets/Resources/"))
        {
            path = path.Remove(0, ASSETS_RESOURCES_FOLDER_LENGTH);
        }
        else
        {
            Debug.LogWarning("Объект должен находиться в папке \"Resources\".");
            return string.Empty;
        }

        System.Type assetType = asset.GetType();

        if (expectedType == assetType ||
            expectedType.IsAssignableFrom(assetType))
        {
            return path;
        }

        if (asset is GameObject gameObject &&
            typeof(UnityEngine.Component).IsAssignableFrom(expectedType))
        {
            if (gameObject.TryGetComponent(expectedType, out _))
            {
                return path;
            }

            Debug.LogWarning($"На объекте нет компоненета \"{expectedType}\".");
            return string.Empty;
        }

        Debug.LogWarning($"Требуется объект типа \"{expectedType}\".");
        return string.Empty;
    }

    private static string RemoveExtension(string path)
    {
        for (int i = path.Length - 1; i >= 0; i--)
        {
            if (path[i] == '.')
            {
                return path[..i];
            }
        }

        return path;
    }
}

#endif
