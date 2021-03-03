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
