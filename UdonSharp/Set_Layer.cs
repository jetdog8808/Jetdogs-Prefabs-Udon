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
using UnityEngine.UI; //needs to be added to interact with unity ui components.
using VRC.SDKBase;
using VRC.Udon;

#if !COMPILER_UDONSHARP && UNITY_EDITOR
using UnityEditor;
using UdonSharpEditor;
#endif

namespace JetDog.Prefabs
{
public class Set_Layer : UdonSharpBehaviour
{
    public int layer;
    public Dropdown dropdown;
    public GameObject[] gameobjects;

    private void Start()
    {
        if (dropdown) dropdown.SetValueWithoutNotify(layer);
    }
    public virtual void Interact()
    {
        SetLayer();
    }

    public void SetLayer()
    {

        if (dropdown)
        {
            layer = dropdown.value;
        }

        for (int i = 0; i < gameobjects.Length; i++)
        {
            gameobjects[i].layer = layer;
        }
    }
}

#if !COMPILER_UDONSHARP && UNITY_EDITOR

[CustomEditor(typeof(Set_Layer))]
public class Set_Layer_Editor : Editor
{
    SerializedProperty layerProperty;
    SerializedProperty dropDownProperty;
    SerializedProperty gameobjectsProperty;

    Set_Layer inspectorBehaviour;
    string[] layers;

    private void OnEnable()
    {
        inspectorBehaviour = (Set_Layer)target;

        layerProperty = serializedObject.FindProperty(nameof(Set_Layer.layer));
        dropDownProperty = serializedObject.FindProperty(nameof(Set_Layer.dropdown));
        gameobjectsProperty = serializedObject.FindProperty(nameof(Set_Layer.gameobjects));

        layers = new string[32];
        for( int i = 0; i < layers.Length; i++)
        {
            layers[i] = LayerMask.LayerToName(i);
        }

    }

    public override void OnInspectorGUI()
    {
        if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;


        layerProperty.intValue = EditorGUILayout.Popup("Layer", layerProperty.intValue, layers);
        //EditorGUILayout.PropertyField(layerProperty);
        EditorGUILayout.PropertyField(dropDownProperty);
        EditorGUILayout.PropertyField(gameobjectsProperty);


        serializedObject.ApplyModifiedProperties();

    }



}
#endif
}

