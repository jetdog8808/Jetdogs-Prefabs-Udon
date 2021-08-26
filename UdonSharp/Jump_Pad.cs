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
public class Jump_Pad : UdonSharpBehaviour
{
    public float strength = 10;

    //used if the collider is a trigger.
    public override void OnPlayerTriggerEnter(VRC.SDKBase.VRCPlayerApi player)
    {
        if (player.isLocal)
        {
            PlayerSetVelocity();
        }
    }

    //used if the collider is not a trigger.
    public override void OnPlayerCollisionEnter(VRC.SDKBase.VRCPlayerApi player)
    {

        if (player.isLocal)
        {
            PlayerSetVelocity();
        }
    }

    private void PlayerSetVelocity()
    {
        Networking.LocalPlayer.SetVelocity(transform.rotation * Vector3.up * strength);
    }
}
}

