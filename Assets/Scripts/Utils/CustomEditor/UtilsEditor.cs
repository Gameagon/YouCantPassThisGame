#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UtilityEditor
{
    [CustomEditor(typeof(MonoBehaviour))]
    public class UtilsEditor : Editor
    {
        public class TextPopup : EditorWindow
        {
            Action<string> _callback;
            string _text;

            string inputText = "";

            private void OnGUI()
            {
            
                GUILayout.Space(10);
                EditorGUILayout.LabelField(_text, EditorStyles.wordWrappedLabel);
                GUILayout.Space(10);
                //Debug.Log(EditorGUILayout.TextField("", inputText));
                inputText = EditorGUILayout.TextField("", inputText);
                GUILayout.Space(60);

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Save"))
                {
                    //Debug.Log(inputText);
                    _callback(inputText);
                    Close();
                }

                if (GUILayout.Button("Cancel")) Close();
                EditorGUILayout.EndHorizontal();
            }

            public TextPopup(Action<string> method, string text = "Text here:")
            {
                _callback = method;
                _text = text;
            }
        }
    }
}

#endif

