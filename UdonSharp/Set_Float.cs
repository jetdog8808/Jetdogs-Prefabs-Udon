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
public class Set_Float : UdonSharpBehaviour
{
    public UdonBehaviour udonBehaviour;
    public string variableName;
    [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(OnValue))]
    public float value_f;
    public float OnValue
    {
        set
        {
            value_f = value;

            if (udonBehaviour)
            {
                udonBehaviour.SetProgramVariable(variableName, value_f);

            }

            if (slider)
            {
                slider.SetValueWithoutNotify(value_f);
            }

            UpdateDisplay();

        }

        get => value_f;
    }

    public bool getOnStart = false;
    public bool sync = false;
    public float step = 1f;
    public string format = "0.00";

    [Tooltip("Unity UI or TMPro")]
    public MaskableGraphic display;
    public Slider slider;

    private float originalValue;

    void Start()
    {
        if (getOnStart)
        {
            if (udonBehaviour)
            {
                originalValue = (float)udonBehaviour.GetProgramVariable(variableName);
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
            OnValue = slider.value;
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
            ((Text)display).text = value_f.ToString(format);
        }
        else if (type == typeof(TextMeshPro))
        {
            ((TextMeshPro)display).text = value_f.ToString(format);
        }
        else if (type == typeof(TextMeshProUGUI))
        {
            ((TextMeshProUGUI)display).text = value_f.ToString(format);
        }

    }


}

#if !COMPILER_UDONSHARP && UNITY_EDITOR

[CustomEditor(typeof(Set_Float))]
public class Set_Float_Editor : Editor
{
    SerializedProperty udonbehaviourProperty;
    SerializedProperty variableNameProperty;
    SerializedProperty value_fProperty;
    SerializedProperty getOnStartProperty;
    SerializedProperty syncProperty;
    SerializedProperty stepProperty;
    SerializedProperty formatProperty;
    SerializedProperty displayProperty;
    SerializedProperty sliderProperty;

    public string[] Variables;
    public int selected;
    Set_Float inspectorBehaviour;

    private void OnEnable()
    {
        inspectorBehaviour = (Set_Float)target;

        udonbehaviourProperty = serializedObject.FindProperty(nameof(Set_Float.udonBehaviour));
        variableNameProperty = serializedObject.FindProperty(nameof(Set_Float.variableName));
        value_fProperty = serializedObject.FindProperty(nameof(Set_Float.value_f));
        getOnStartProperty = serializedObject.FindProperty(nameof(Set_Float.getOnStart));
        syncProperty = serializedObject.FindProperty(nameof(Set_Float.sync));
        stepProperty = serializedObject.FindProperty(nameof(Set_Float.step));
        formatProperty = serializedObject.FindProperty(nameof(Set_Float.format));
        displayProperty = serializedObject.FindProperty(nameof(Set_Float.display));
        sliderProperty = serializedObject.FindProperty(nameof(Set_Float.slider));


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

        EditorGUILayout.PropertyField(value_fProperty);
        EditorGUILayout.PropertyField(stepProperty);
        EditorGUILayout.PropertyField(getOnStartProperty);
        EditorGUILayout.PropertyField(syncProperty);
        EditorGUILayout.PropertyField(formatProperty);
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
                Variables = properties.Where(property => (property.Module.Name == "Assembly-CSharp.dll") && (property.FieldType == typeof(float))).Select(property => property.Name).ToArray();
                return true;

            }
        }

        return false;
    }
}
#endif

}
