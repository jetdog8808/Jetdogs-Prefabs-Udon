
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Sync_Toggle : UdonSharpBehaviour
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
        Toggle();
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
