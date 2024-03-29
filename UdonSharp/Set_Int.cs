﻿/*
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
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;
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
public class Set_Int : UdonSharpBehaviour
{
    public UdonBehaviour udonBehaviour;
    public string variableName;
    [UdonSynced(UdonSyncMode.None)]
    public int value_i;
    public int OnValue
    {
        set
        {
            value_i = value;

            if (udonBehaviour)
            {
                udonBehaviour.SetProgramVariable(variableName, value_i);

            }

            if (slider)
            {
                slider.SetValueWithoutNotify(value_i);
            }

            UpdateDisplay();

        }

        get => value_i;
    }

    public bool getOnStart = false;
    public bool sync = false;
    public int step = 1;

    [Tooltip("Unity UI or TMPro")]
    public MaskableGraphic display;
    public Slider slider;

    private int originalValue;

    void Start()
    {
        if (getOnStart)
        {
            if (udonBehaviour)
            {
                originalValue = (int)udonBehaviour.GetProgramVariable(variableName);
            }

            OnValue = originalValue;
        }
        else
        {
            originalValue = OnValue;
        }

        step = Mathf.Abs(step);
    }

    public void Increase()
    {
        OnValue += step;
        SyncV();
    }

    public void Decrease()
    {
        OnValue -= step;
        SyncV();
    }

    public void ResetValue()
    {
        OnValue = originalValue;
        SyncV();
    }

    public void SetSlider()
    {
        if (slider)
        {
            OnValue = Mathf.RoundToInt(slider.value);
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
            ((Text)display).text = value_i.ToString();
        }
        else if (type == typeof(TextMeshPro))
        {
            ((TextMeshPro)display).text = value_i.ToString();
        }
        else if (type == typeof(TextMeshProUGUI))
        {
            ((TextMeshProUGUI)display).text = value_i.ToString();
        }

    }

}

#if !COMPILER_UDONSHARP && UNITY_EDITOR

[CustomEditor(typeof(Set_Int))]
public class Set_Int_Editor : Editor
{
    SerializedProperty udonbehaviourProperty;
    SerializedProperty variableNameProperty;
    SerializedProperty value_iProperty;
    SerializedProperty getOnStartProperty;
    SerializedProperty syncProperty;
    SerializedProperty stepProperty;
    SerializedProperty displayProperty;
    SerializedProperty sliderProperty;

    public string[] Variables;
    public int selected;
    Set_Int inspectorBehaviour;

    private void OnEnable()
    {
        inspectorBehaviour = (Set_Int)target;

        udonbehaviourProperty = serializedObject.FindProperty(nameof(Set_Int.udonBehaviour));
        variableNameProperty = serializedObject.FindProperty(nameof(Set_Int.variableName));
        value_iProperty = serializedObject.FindProperty(nameof(Set_Int.value_i));
        getOnStartProperty = serializedObject.FindProperty(nameof(Set_Int.getOnStart));
        syncProperty = serializedObject.FindProperty(nameof(Set_Int.sync));
        stepProperty = serializedObject.FindProperty(nameof(Set_Int.step));
        displayProperty = serializedObject.FindProperty(nameof(Set_Int.display));
        sliderProperty = serializedObject.FindProperty(nameof(Set_Int.slider));


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
            if (inspectorBehaviour.udonBehaviour.programSource is UdonSharpProgramAsset)
            {
                selected = EditorGUILayout.Popup("Variables", selected, Variables);
                if (Variables.Length == 0)
                {
                    inspectorBehaviour.variableName = string.Empty;
                }
                else
                {
                    inspectorBehaviour.variableName = Variables[selected];
                }

            }
            else
            {
                EditorGUILayout.PropertyField(variableNameProperty);

            }
        }

        EditorGUILayout.PropertyField(value_iProperty);
        EditorGUILayout.PropertyField(stepProperty);
        EditorGUILayout.PropertyField(getOnStartProperty);
        EditorGUILayout.PropertyField(syncProperty);
        EditorGUILayout.PropertyField(displayProperty);
        EditorGUILayout.PropertyField(sliderProperty);


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
                Variables = properties.Where(property => (property.Module.Name == "Assembly-CSharp.dll") && (property.FieldType == typeof(int))).Select(property => property.Name).ToArray();
                return true;

            }
        }

        return false;
    }
}
#endif

}
