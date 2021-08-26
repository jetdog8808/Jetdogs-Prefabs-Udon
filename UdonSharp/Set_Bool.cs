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
public class Set_Bool : UdonSharpBehaviour
{
    public UdonBehaviour udonBehaviour;
    public string variableName;
    [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(OnValue))]
    public bool value_b;
    public bool OnValue
    {
        set
        {
            value_b = value;

            if (udonBehaviour)
            {
                udonBehaviour.SetProgramVariable(variableName, value_b);

            }

            if (toggle)
            {
                toggle.SetIsOnWithoutNotify(value_b);
            }

            UpdateDisplay();

        }

        get => value_b;
    }

    
    public bool getOnStart = false;
    public bool sync = false;

    [Tooltip("Unity UI or TMPro")]
    public MaskableGraphic display;
    public Toggle toggle;

    void Start()
    {
        if (getOnStart && udonBehaviour)
        {
            OnValue = (bool)udonBehaviour.GetProgramVariable(variableName);
        }

    }

    public void SetTrue()
    {
        OnValue = true;
        SyncV();
    }

    public void SetFalse()
    {
        OnValue = false;
        SyncV();
    }

    public void Toggle()
    {
        OnValue = !OnValue;
        SyncV();
    }

    public void SetToggle()
    {
        if (toggle)
        {
            OnValue = toggle.isOn;
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

        if(type == typeof(Text))
        {
            ((Text)display).text = value_b.ToString();
        }
        else if(type == typeof(TextMeshPro))
        {
            ((TextMeshPro)display).text = value_b.ToString();
        }
        else if(type == typeof(TextMeshProUGUI))
        {
            ((TextMeshProUGUI)display).text = value_b.ToString();
        }
        
    }
}

#if !COMPILER_UDONSHARP && UNITY_EDITOR

[CustomEditor(typeof(Set_Bool))]
public class Set_Bool_Editor : Editor
{
    SerializedProperty udonbehaviourProperty;
    SerializedProperty variableNameProperty;
    SerializedProperty value_bProperty;
    SerializedProperty getOnStartProperty;
    SerializedProperty syncProperty;
    SerializedProperty displayProperty;
    SerializedProperty toggleProperty;

    public string[] Variables;
    public int selected;
    Set_Bool inspectorBehaviour;

    private void OnEnable()
    {
        inspectorBehaviour = (Set_Bool)target;

        udonbehaviourProperty = serializedObject.FindProperty(nameof(Set_Bool.udonBehaviour));
        variableNameProperty = serializedObject.FindProperty(nameof(Set_Bool.variableName));
        value_bProperty = serializedObject.FindProperty(nameof(Set_Bool.value_b));
        getOnStartProperty = serializedObject.FindProperty(nameof(Set_Bool.getOnStart));
        syncProperty = serializedObject.FindProperty(nameof(Set_Bool.sync));
        displayProperty = serializedObject.FindProperty(nameof(Set_Bool.display));
        toggleProperty = serializedObject.FindProperty(nameof(Set_Bool.toggle));


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

            //Debug.Log("Finished event stuff");
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

        EditorGUILayout.PropertyField(value_bProperty);
        EditorGUILayout.PropertyField(getOnStartProperty);
        EditorGUILayout.PropertyField(syncProperty);
        EditorGUILayout.PropertyField(displayProperty);
        EditorGUILayout.PropertyField(toggleProperty);


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
                Variables = properties.Where(property => (property.Module.Name == "Assembly-CSharp.dll") && (property.FieldType == typeof(bool))).Select(property => property.Name).ToArray();
                return true;

            }
        }

        return false;
    }
}
#endif
}

