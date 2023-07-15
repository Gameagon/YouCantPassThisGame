using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SingleEnumFlagSelectAttribute : PropertyAttribute
{
    private Type enumType;

    public Type EnumType
    {
        get => enumType;
        set
        {
            if (value == null)
            {
                Debug.LogError($"{GetType().Name}: EnumType cannot be null");
                return;
            }
            if (!value.IsEnum)
            {
                Debug.LogError($"{GetType().Name}: EnumType is {value.Name} this is not an enum");
                return;
            }
            enumType = value;
            IsValid = true;
        }
    }

    public bool IsValid { get; private set; }
}

[CustomPropertyDrawer(typeof(SingleEnumFlagSelectAttribute))]
public class SingleEnumFlagSelectAttributeEditor : PropertyDrawer
{
    public override void OnGUI(Rect position,
      SerializedProperty property, GUIContent label)
    {
        var singleEnumFlagSelectAttribute =
          (SingleEnumFlagSelectAttribute)attribute;
        if (!singleEnumFlagSelectAttribute.IsValid)
        {
            return;
        }
        var displayTexts = new List<GUIContent>();
        var enumValues = new List<int>();
        foreach (var displayText in
          Enum.GetValues(singleEnumFlagSelectAttribute.EnumType))
        {
            displayTexts.Add(new GUIContent(displayText.ToString()));
            enumValues.Add((int)displayText);
        }

        property.intValue = EditorGUI.IntPopup(position, label, property.intValue,
            displayTexts.ToArray(), enumValues.ToArray());
    }
}
