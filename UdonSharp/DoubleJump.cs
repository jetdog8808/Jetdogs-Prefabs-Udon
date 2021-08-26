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
public class DoubleJump : UdonSharpBehaviour
{
    public bool enable = true;
    public float secondJumpMultiplier = 1f;

    private bool doublejump = true;
    private VRCPlayerApi user;

    void Start()
    {
        //cache local user reference.
        user = Networking.LocalPlayer;
    }

    //vrc event when player does the jump input.
    public override void InputJump(bool value, VRC.Udon.Common.UdonInputEventArgs args)
    {
        if (enable && value)
        {
            /* checks is jumping while on the ground.
             * if true record it
             * if you jumped and and not on the ground does double jump.
             * 
             * this does not account for coyote time wanted this to be simple.
             */
            if (user.IsPlayerGrounded())
            {
                doublejump = true;
            }
            else if(doublejump)
            {
                doublejump = false;
                Vector3 dVelocity = user.GetVelocity();
                dVelocity.y = user.GetJumpImpulse() * secondJumpMultiplier;
                user.SetVelocity(dVelocity);
            }

        }


    }
}
}

