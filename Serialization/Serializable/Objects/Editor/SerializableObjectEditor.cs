#if ENABLE_SERIALIZATION && UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using DamnLibrary.Serialization;
using UnityEditor;
using UnityEngine;

namespace DamnLibrary.Serialization
{
    [CustomEditor(typeof(SerializableObject)), CanEditMultipleObjects]
    public class SerializableObjectEditor : Editor
    {
        private SerializedProperty serializeAllComponentsProperty;
        private SerializedProperty serializableComponentsProperty;
        private SerializedProperty serializableIdProperty;
        private SerializedProperty scriptProperty;

        private void OnEnable()
        {
            scriptProperty = serializedObject.FindProperty("m_Script");
            serializableIdProperty = serializedObject.FindProperty("serializableId");
            serializeAllComponentsProperty = serializedObject.FindProperty("serializeAllComponents");
            serializableComponentsProperty = serializedObject.FindProperty("serializableComponents");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            GUI.enabled = false;
            EditorGUILayout.PropertyField(scriptProperty, true);
            GUI.enabled = true;
            EditorGUILayout.PropertyField(serializableIdProperty);
            EditorGUILayout.PropertyField(serializeAllComponentsProperty);
            if (!serializeAllComponentsProperty.boolValue)
                EditorGUILayout.PropertyField(serializableComponentsProperty);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif