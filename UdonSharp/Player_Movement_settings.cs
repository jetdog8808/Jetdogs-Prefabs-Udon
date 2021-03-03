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

public class Player_Movement_settings : UdonSharpBehaviour
{
    public float jumpImpulse = 3;
    public float walkSpeed = 2;
    public float strafeSpeed = 2;
    public float runSpeed = 4;
    public float gravityStrength = 1;
    public bool legacyLocomotion = false;

    public bool setOnStart = true;

    public Player_Movement_settings zoneExitReset;

    void Start()
    {
        if (setOnStart)
        {
            SetMovement();

            if (legacyLocomotion)
            {
                Networking.LocalPlayer.UseLegacyLocomotion();
            }
        }
    }

    public void SetMovement()
    {
        VRCPlayerApi localuser = Networking.LocalPlayer;
        localuser.SetJumpImpulse(jumpImpulse);
        localuser.SetWalkSpeed(walkSpeed);
        localuser.SetStrafeSpeed(strafeSpeed);
        localuser.SetRunSpeed(runSpeed);
        localuser.SetGravityStrength(gravityStrength);
    }

    public virtual void OnPlayerTriggerEnter(VRC.SDKBase.VRCPlayerApi player) 
    {
        if (player.isLocal)
        {
            SetMovement();
        }
    }
    public virtual void OnPlayerTriggerExit(VRC.SDKBase.VRCPlayerApi player) 
    { 
        if(player.isLocal && zoneExitReset)
        {
            zoneExitReset.SetMovement();
        }
    }
}
