
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class String_password : UdonSharpBehaviour
{
    public string password = "0123";
    public InputField inputfield;

    public UdonBehaviour udonBehaviour; //the udonebehaviour you will be calling the event on
    public string eventName; //string name of the event. 
    public bool networkEvent; //bool of wheather the event gets called localy or with a network event.
    public VRC.Udon.Common.Interfaces.NetworkEventTarget target; //if using a network event who the target will be.

    public void submit() //the event sending out the udonbehaviour event.
    {
        if(inputfield.text == password)
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
        else
        {
            inputfield.text = string.Empty;
        }
        
    }
}
