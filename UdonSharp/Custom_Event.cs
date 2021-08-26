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
public class Custom_Event : UdonSharpBehaviour
{
    public UdonBehaviour udonBehaviour; //the udonebehaviour you will be calling the event on
    public string eventName; //string name of the event. 
    public bool networkEvent; //bool of whether the event gets called localy or with a network event.
    public VRC.Udon.Common.Interfaces.NetworkEventTarget nTarget; //if using a network event who the target will be.

    public override void Interact() //get called when the player interacts with a collider.
    {
        SendEvent(); //callling a method in the same script.
    }

    public void SendEvent() //the event sending out the udonbehaviour event.
    {
        if (!udonBehaviour || eventName == null) return;

        if (networkEvent) 
        {
            udonBehaviour.SendCustomNetworkEvent(nTarget, eventName);
        }
        else
        {
            udonBehaviour.SendCustomEvent(eventName);
        }
    }



}

//custom editor for udonbehaviour
#if !COMPILER_UDONSHARP && UNITY_EDITOR

[CustomEditor(typeof(Custom_Event))]
public class Custom_Event_Editor : Editor
{
    SerializedProperty udonbehaviourProperty;
    SerializedProperty eventNameProperty;
    SerializedProperty networkedEventProperty;
    SerializedProperty nTargetProperty;

    public string[] events;
    public int selected;
    Custom_Event inspectorBehaviour;

    private void OnEnable()
    {
        inspectorBehaviour = (Custom_Event)target;

        udonbehaviourProperty = serializedObject.FindProperty(nameof(Custom_Event.udonBehaviour));
        eventNameProperty = serializedObject.FindProperty(nameof(Custom_Event.eventName));
        networkedEventProperty = serializedObject.FindProperty(nameof(Custom_Event.networkEvent));
        nTargetProperty = serializedObject.FindProperty(nameof(Custom_Event.nTarget));

        if (CacheMethods())
        {
            selected = Array.IndexOf(events, inspectorBehaviour.eventName);
        }
        //Debug.Log("fired on enable");
    }

    public override void OnInspectorGUI()
    {
        if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;


        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(udonbehaviourProperty);

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();

            if (CacheMethods())
            {
                selected = 0;
            }

            //Debug.Log("Finished event stuff");
        }

        if (inspectorBehaviour.udonBehaviour)
        {
            if (inspectorBehaviour.udonBehaviour.programSource is UdonSharpProgramAsset)
            {
                selected = EditorGUILayout.Popup("Events", selected, events);
                if(events.Length == 0)
                {
                    inspectorBehaviour.eventName = string.Empty;
                }
                else
                {
                    inspectorBehaviour.eventName = events[selected];
                }
                
            }
            else
            {
                EditorGUILayout.PropertyField(eventNameProperty);

            }
        }

        EditorGUILayout.PropertyField(networkedEventProperty);

        if (inspectorBehaviour.networkEvent)
        {
            EditorGUILayout.PropertyField(nTargetProperty);
        }


        serializedObject.ApplyModifiedProperties();

        /*
        if (GUILayout.Button("debbuging"))
        {
            UdonSharpProgramAsset u_asset = inspectorBehaviour.udonBehaviour.programSource as UdonSharpProgramAsset;
            if (u_asset)
            {
                System.Reflection.MethodInfo[] methods = u_asset.GetClass().GetMethods();
                foreach (System.Reflection.MethodInfo method in methods)
                {
                    Debug.Log(method.Name + "||" + method.Module + "||" + method.GetParameters().Length + "||" + method.ReturnType + "||" + method.IsVirtual);
                }

            }
        }*/

    }

    public bool CacheMethods()
    {
        if (inspectorBehaviour.udonBehaviour)
        {
            UdonSharpProgramAsset u_asset = inspectorBehaviour.udonBehaviour.programSource as UdonSharpProgramAsset;
            if (u_asset)
            {
                System.Reflection.MethodInfo[] methods = u_asset.GetClass().GetMethods();
                events = methods.Where(method => (method.Module.Name == "Assembly-CSharp.dll") && (method.GetParameters().Length == 0) && (method.ReturnType == typeof(void)) && method.IsPublic && !method.IsVirtual).Select(method => method.Name).ToArray();
                return true;

            }
        }

        return false;
    }
}
#endif
}


