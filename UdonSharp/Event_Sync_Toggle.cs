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

public class Event_Sync_Toggle : UdonSharpBehaviour
{
    public GameObject[] gameobjectArray;
    private bool state = false;

    void Start()
    {
        if (!Networking.IsMaster)
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "RequestState");
        }        
    }

    public virtual void Interact() 
    {
        if (!state)
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "OnToggle");
        }
        else
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "OffToggle");
        }
    }

    public void Toggle()
    {
        state = !state;
        for (int i = 0; i < gameobjectArray.Length; i++)
        {
            gameobjectArray[i].SetActive(!gameobjectArray[i].activeSelf);
        }
    }

    public void RequestState()
    {
        if (state)
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "OnToggle");
        }
        else
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "OffToggle");
        }
    }

    public void OnToggle()
    {
        if (!state)
        {
            Toggle();
        }
    }

    public void OffToggle()
    {
        if (state)
        {
            Toggle();
        }
    }
}
