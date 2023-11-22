using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Game.Utils.Attributes.ShowInInspectorAttribute))]
public sealed class ShowInInspectorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        MethodInfo methodInfo = fieldInfo.DeclaringType.GetMethod(fieldInfo.Name);
        if (methodInfo != null)
        {
            object value = methodInfo.Invoke(property.serializedObject.targetObject, null);
            EditorGUI.LabelField(position, label.text, value.ToString());
        }
    }
}
