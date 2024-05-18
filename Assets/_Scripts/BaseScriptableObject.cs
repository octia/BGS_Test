using System;
using UnityEditor;
using UnityEngine;

// StackOverflow code, I want to use the ID for uniquely identifying item types, instead of using names.
namespace BGSTest
{
    public class ScriptableObjectIdAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ScriptableObjectIdAttribute))]
public class ScriptableObjectIdDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        GUI.enabled = false;
        if (string.IsNullOrEmpty(property.stringValue)) {
            property.stringValue = Guid.NewGuid().ToString();
        }
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
#endif

    public class BaseScriptableObject : ScriptableObject
    {
        [ScriptableObjectId]
        public string Id;
    }
}