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


public class Join_Bell : UdonSharpBehaviour
{
    public AudioSource bell;
    public bool oneshot = false;
    public bool networked = false;

    void Start()
    {
        if (networked)
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Joined");
        }
    }

    public virtual void OnPlayerJoined(VRC.SDKBase.VRCPlayerApi player) 
    {
        if (!networked)
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
