/*
*===========================================================*
*       _      _   ____              _          _           *
*      | | ___| |_|  _ \  ___   __ _| |    __ _| |__  ___   *
*   _  | |/ _ \ __| | | |/ _ \ / _` | |   / _` | '_ \/ __|  *
*  | |_| |  __/ |_| |_| | (_) | (_| | |__| (_| | |_) \__ \  *
*   \___/ \___|\__|____/ \___/ \__, |_____\__,_|_.__/|___/  *
*                              |___/                        *
*===========================================================*
*                                                           *
*                  Auther: Jetdog8808                       *
*                                                           *
*===========================================================*
*/

using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using TMPro;
using System;

#if !COMPILER_UDONSHARP && UNITY_EDITOR
using UnityEditor;
using UdonSharpEditor;
using System.Linq;

#endif

namespace JetDog.Prefabs
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class Set_String : UdonSharpBehaviour
    {
        public UdonBehaviour udonBehaviour;
        public string variableName;
        [UdonSynced(UdonSyncMode.None)]
        public string value_s;
        public string OnValue
        {
            set
            {
                value_s = value;

                if (udonBehaviour)
                {
                    udonBehaviour.SetProgramVariable(variableName, value_s);

                }

                if (inputfield)
                {
                    inputfield.text = value_s;
                }

                UpdateDisplay();

            }

            get => value_s;
        }

        public bool getOnStart = false;
        public bool sync = false;

        [Tooltip("Unity UI or TMPro")]
        public MaskableGraphic display;
        public InputField inputfield;

        private string originalValue;

        void Start()
        {
            if (getOnStart)
            {
                if (udonBehaviour)
                {
                    originalValue = (string)udonBehaviour.GetProgramVariable(variableName);
                }

                OnValue = originalValue;
            }
            else
            {
                originalValue = OnValue;
            }
        }

        public void SetValue()
        {
            OnValue = OnValue;
        }

        public void ResetValue()
        {
            OnValue = originalValue;
            SyncV();
        }

        public void SetInputField()
        {
            if (inputfield)
            {
                OnValue = inputfield.text;
                SyncV();
            }
        }

        public void SyncV()
        {
            if (sync)
            {
                Networking.SetOwner(Networking.LocalPlayer, gameObject);
                RequestSerialization();
            }
        }

        private void UpdateDisplay()
        {

            if (!display)
            {
                return;
            }

            Type type = display.GetType();

            if (type == typeof(Text))
            {
                ((Text)display).text = value_s;
            }
            else if (type == typeof(TextMeshPro))
            {
                ((TextMeshPro)display).text = value_s;
            }
            else if (type == typeof(TextMeshProUGUI))
            {
                ((TextMeshProUGUI)display).text = value_s;
            }

        }
    }

#if !COMPILER_UDONSHARP && UNITY_EDITOR

    [CustomEditor(typeof(Set_String))]
    public class Set_String_Editor : Editor
    {
        SerializedProperty udonbehaviourProperty;
        SerializedProperty variableNameProperty;
        SerializedProperty value_sProperty;
        SerializedProperty getOnStartProperty;
        SerializedProperty syncProperty;
        SerializedProperty displayProperty;
        SerializedProperty inputfieldProperty;

        public string[] variables;
        public int selected;
        Set_String inspectorBehaviour;

        private void OnEnable()
        {
            inspectorBehaviour = (Set_String)target;

            udonbehaviourProperty = serializedObject.FindProperty(nameof(Set_String.udonBehaviour));
            variableNameProperty = serializedObject.FindProperty(nameof(Set_String.variableName));
            value_sProperty = serializedObject.FindProperty(nameof(Set_String.value_s));
            getOnStartProperty = serializedObject.FindProperty(nameof(Set_String.getOnStart));
            syncProperty = serializedObject.FindProperty(nameof(Set_String.sync));
            displayProperty = serializedObject.FindProperty(nameof(Set_String.display));
            inputfieldProperty = serializedObject.FindProperty(nameof(Set_String.inputfield));


            if (CacheProperties())
            {
                selected = Array.IndexOf(variables, inspectorBehaviour.variableName);
            }
        }

        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;


            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(udonbehaviourProperty);

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();

                if (CacheProperties())
                {
                    selected = 0;
                }

            }

            if (inspectorBehaviour.udonBehaviour)
            {
                if (inspectorBehaviour.udonBehaviour.programSource is UdonSharpProgramAsset)
                {
                    selected = EditorGUILayout.Popup("Variables", selected, variables);
                    if (variables.Length == 0)
                    {
                        inspectorBehaviour.variableName = string.Empty;
                    }
                    else
                    {
                        inspectorBehaviour.variableName = variables[selected];
                    }

                }
                else
                {
                    EditorGUILayout.PropertyField(variableNameProperty);

                }
            }

            EditorGUILayout.PropertyField(value_sProperty);
            EditorGUILayout.PropertyField(getOnStartProperty);
            EditorGUILayout.PropertyField(syncProperty);
            EditorGUILayout.PropertyField(displayProperty);
            EditorGUILayout.PropertyField(inputfieldProperty);


            serializedObject.ApplyModifiedProperties();

        }

        public bool CacheProperties()
        {
            if (inspectorBehaviour.udonBehaviour)
            {
                UdonSharpProgramAsset u_asset = inspectorBehaviour.udonBehaviour.programSource as UdonSharpProgramAsset;
                if (u_asset)
                {
                    System.Reflection.FieldInfo[] properties = u_asset.GetClass().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                    variables = properties.Where(property => (property.Module.Name == "Assembly-CSharp.dll") && (property.FieldType == typeof(string))).Select(property => property.Name).ToArray();
                    return true;

                }
            }

            return false;
        }
    }
#endif
}

