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

public class Jump_Pad : UdonSharpBehaviour
{
    public Vector3 direction = Vector3.up;
    public float strength = 10;


    void Start()
    {
        direction = direction.normalized * strength;
    }

    public virtual void OnPlayerTriggerEnter(VRC.SDKBase.VRCPlayerApi player)
    {
        if (player.isLocal)
        {
            playersetvelocity();
        }
    }

    public virtual void OnPlayerTriggerStay(VRC.SDKBase.VRCPlayerApi player)
    {
        if (player.isLocal)
        {
            playersetvelocity();
        }
    }

    public void playersetvelocity()
    {
        Networking.LocalPlayer.SetVelocity(direction);
    }
}
