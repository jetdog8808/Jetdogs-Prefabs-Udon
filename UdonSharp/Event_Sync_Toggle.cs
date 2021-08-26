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

namespace JetDog.Prefabs
{
public class Event_Sync_Toggle : UdonSharpBehaviour
{
    public GameObject[] gameobjectArray;
    //local state of toggle
    private bool state = false;

    void Start()
    {
        //request master what state things are.
        if (!Networking.IsMaster)
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, nameof(RequestState));
        }        
    }

    public override void Interact() 
    {
        //send a event of the state you are switching too.
        if (!state)
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(OnToggle));
        }
        else
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(OffToggle));
        }
    }

    public void Toggle()
    {
        //invert your current state of objects.
        state = !state;
        for (int i = 0; i < gameobjectArray.Length; i++)
        {
            gameobjectArray[i].SetActive(!gameobjectArray[i].activeSelf);
        }
    }

    public void RequestState()
    {
        //master replying back what state they are at.
        if (state)
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(OnToggle));
        }
        else
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(OffToggle));
        }
    }

    public void OnToggle()
    {
        //if state is off switch it.
        if (!state)
        {
            Toggle();
        }
    }

    public void OffToggle()
    {
        //if state is on switch it.
        if (state)
        {
            Toggle();
        }
    }
}
}

