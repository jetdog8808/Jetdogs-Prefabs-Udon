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
public class Join_Bell : UdonSharpBehaviour
{
    public AudioSource bell;
    public bool oneshot = false;
    public bool networkEvent = false;

    void Start()
    {
        if (networkEvent)
        {
            //you can choose to sound the bell by sending a network event to everyone.
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Joined");
        }
    }

    //you can choose if to sound the bell on the player join event.
    public override void OnPlayerJoined(VRC.SDKBase.VRCPlayerApi player) 
    {
        if (!networkEvent)
        {
            Joined();
        }
    }

    public void Joined()
    {
        if (oneshot)
        {
            bell.PlayOneShot(bell.clip, bell.volume);
        }
        else
        {
            bell.Play();
        }
    }
}
}

