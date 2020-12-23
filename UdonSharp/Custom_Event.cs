
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Custom_Event : UdonSharpBehaviour
{
    public UdonBehaviour udonBehaviour; //the udonebehaviour you will be calling the event on
    public string eventName; //string name of the event. 
    public bool networkEvent; //bool of wheather the event gets called localy or with a network event.
    public VRC.Udon.Common.Interfaces.NetworkEventTarget target; //if using a network event who the target will be.

    public virtual void Interact() //get called when the player interacts with a collider.
    {
        SendEvent(); //callling a method in the same script.
    }

    public void SendEvent() //the event sending out the udonbehaviour event.
    {
        if (networkEvent) 
        {
            udonBehaviour.SendCustomNetworkEvent(target, eventName);
        }
        else
        {
            udonBehaviour.SendCustomEvent(eventName);
        }
    }

}
