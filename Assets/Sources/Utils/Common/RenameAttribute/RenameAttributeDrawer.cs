#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RenameAttribute))]
public class RenameAttributeDrawer : PropertyDrawer
{
    public RenameAttributeDrawer()
    {
        renameAttribute = (RenameAttribute)attribute;
    }

    private readonly RenameAttribute renameAttribute;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label.text = renameAttribute.newName;

        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.PropertyField(position, property, label);
        EditorGUI.EndProperty();
    }
}

#endif
