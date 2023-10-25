#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Interface<>))]
public class InterfaceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        System.Type expectedType = fieldInfo.GetFieldType().GetGenericArguments()[0];

        if (expectedType.IsInterface == false)
        {
            EditorGUI.LabelField(position, "Используйте \"Interface\", передавая как обобщённый аргумент интерфейс.");
            return;
        }

        SerializedProperty valueProperty = property.FindPropertyRelative("_value");


        EditorGUI.BeginProperty(position, label, valueProperty);
        EditorGUI.BeginChangeCheck();

        label.text += $" ({expectedType.Name})";

        bool toAllowSceneObjects =
            EditorUtility.IsPersistent(valueProperty.serializedObject.targetObject) == false;

        Object value = EditorGUI.ObjectField(position, label,
            valueProperty.objectReferenceValue,
            typeof(Object),
            toAllowSceneObjects);

        if (EditorGUI.EndChangeCheck())
        {
            if (value == null)
            {
                valueProperty.objectReferenceValue = null;
            }
            else if (expectedType.IsAssignableFrom(value.GetType()))
            {
                valueProperty.objectReferenceValue = value;
            }
            else if (value is GameObject gameObject)
            {
                var component = gameObject.GetComponent(expectedType);

                if (component)
                {
                    valueProperty.objectReferenceValue = component;
                }
            }
        }

        EditorGUI.EndProperty();
    }
}

#endif
