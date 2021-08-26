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
using System;

#if !COMPILER_UDONSHARP && UNITY_EDITOR
using UnityEditor;
using UdonSharpEditor;
using System.Linq;

#endif

namespace JetDog.Prefabs
{
    public class Set_Object : UdonSharpBehaviour
    {
        public UdonBehaviour udonBehaviour;
        public string variableName;
        public UnityEngine.Object value;

        public override void Interact()
        {
            SetVariable();
        }

        public void SetVariable()
        {
            if (udonBehaviour) udonBehaviour.SetProgramVariable(variableName, value);
        }
    }

#if !COMPILER_UDONSHARP && UNITY_EDITOR

    [CustomEditor(typeof(Set_Object))]
    public class Set_Object_Editor : Editor
    {
        SerializedProperty udonbehaviourProperty;
        SerializedProperty variableNameProperty;
        SerializedProperty valueProperty;

        public string[] Variables;
        public int selected;
        Set_Object inspectorBehaviour;

        private void OnEnable()
        {
            inspectorBehaviour = (Set_Object)target;

            udonbehaviourProperty = serializedObject.FindProperty(nameof(Set_Object.udonBehaviour));
            variableNameProperty = serializedObject.FindProperty(nameof(Set_Object.variableName));
            valueProperty = serializedObject.FindProperty(nameof(Set_Object.value));


            if (CacheProperties())
            {
                selected = Array.IndexOf(Variables, inspectorBehaviour.variableName);
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
                UdonSharpProgramAsset u_asset = inspectorBehaviour.udonBehaviour.programSource as UdonSharpProgramAsset;

                if (u_asset)
                {
                    if(Variables.Length > 0)
                    {
                        selected = EditorGUILayout.Popup("Variables", selected, Variables);
                        inspectorBehaviour.variableName = Variables[selected];

                        Type fieldtype = u_asset.GetClass().GetField(Variables[selected], System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).FieldType;
                        if (inspectorBehaviour.value != null)
                        {
                            if (/*inspectorBehaviour.value.GetType() != fieldtype*/ !fieldtype.IsAssignableFrom(inspectorBehaviour.value.GetType())) inspectorBehaviour.value = null;
                        }
                        
                        inspectorBehaviour.value = EditorGUILayout.ObjectField("value", inspectorBehaviour.value, fieldtype, true);
                    }
                    else
                    {
                        inspectorBehaviour.variableName = string.Empty;
                        inspectorBehaviour.value = null;
                    }
                    

                }
                else
                {
                    EditorGUILayout.PropertyField(variableNameProperty);
                    EditorGUILayout.PropertyField(valueProperty);

                }
            }            

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
                    Variables = properties.Where(property => (property.Module.Name == "Assembly-CSharp.dll") && (property.FieldType.IsSubclassOf(typeof(UnityEngine.Object)))).Select(property => property.Name).ToArray();
                    return true;

                }
            }

            return false;
        }
    }
#endif
}
