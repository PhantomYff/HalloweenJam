#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Interval))]
public class IntervalDrawer : PropertyDrawer
{
    private const float MIN_PROPERTIES_DIFFERENCE = 0.001f;
    private const float MAX_FIELD_SPACING = 1.5f;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float originalHeight = base.GetPropertyHeight(property, label);

        if (EditorGUIUtility.wideMode == false)
        {
            originalHeight *= 2f;
        }

        return originalHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label = EditorGUI.BeginProperty(position, label, property);

        position.height = EditorGUIUtility.singleLineHeight;

        EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        position.x += EditorGUIUtility.labelWidth;
        position.width -= EditorGUIUtility.labelWidth;

        if (EditorGUIUtility.wideMode)
        {
            position.width /= 2f;
            position.width -= MAX_FIELD_SPACING / 2f;
        }

        SerializedProperty min = property.FindPropertyRelative("_min");
        SerializedProperty max = property.FindPropertyRelative("_max");

        // Backup GUI settings.
        float originalLabelWidth = EditorGUIUtility.labelWidth;
        int originalIndentLevel = EditorGUI.indentLevel;

        EditorGUI.indentLevel = 0;

        // "Min" field.
        var minLabel = new GUIContent("Min");
        EditorGUIUtility.labelWidth = EditorStyles.label.CalcSize(minLabel).x;

        if (EditorGUI.PropertyField(position, min, minLabel) == false &&
            min.floatValue >= max.floatValue)
        {
            min.floatValue = max.floatValue - MIN_PROPERTIES_DIFFERENCE;
        }

        if (EditorGUIUtility.wideMode)
        {
            position.x += position.width + MAX_FIELD_SPACING;
        }
        else
        {
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        // "Max" field.
        var maxLabel = new GUIContent("Max");
        EditorGUIUtility.labelWidth = EditorStyles.label.CalcSize(maxLabel).x;

        if (EditorGUI.PropertyField(position, max, maxLabel) == false &&
            max.floatValue <= min.floatValue)
        {
            max.floatValue = min.floatValue + MIN_PROPERTIES_DIFFERENCE;
        }

        // Restore GUI settings.
        EditorGUIUtility.labelWidth = originalLabelWidth;
        EditorGUI.indentLevel = originalIndentLevel;

        EditorGUI.EndProperty();
    }
}

#endif
