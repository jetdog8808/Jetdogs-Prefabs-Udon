
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Username_event : UdonSharpBehaviour
{
    public string userName;
    public bool localOnly = false;
    public bool onLeave = false;
    public UdonBehaviour udonBehaviour;
    public string eventName;
    
    public virtual void OnPlayerJoined(VRC.SDKBase.VRCPlayerApi player) 
    { 
        if (player.displayName == userName)
        {
            if(player.isLocal || !localOnly)
            {
                udonBehaviour.SendCustomEvent(eventName);
            }
        }
    }
    public virtual void OnPlayerLeft(VRC.SDKBase.VRCPlayerApi player) 
    {
        if (onLeave)
        {
            if (player.displayName == userName)
            {
                if (player.isLocal || !localOnly)
                {
                    udonBehaviour.SendCustomEvent(eventName);
                }
            }
        }
    }
}
