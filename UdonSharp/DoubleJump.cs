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

public class DoubleJump : UdonSharpBehaviour
{
    public bool enable = true;
    public float secondJumpMultiplier = 1f;
    private bool doublejump = true;
    private VRCPlayerApi user;

    void Start()
    {
        user = Networking.LocalPlayer;
    }

    public virtual void InputJump(bool value, VRC.Udon.Common.UdonInputEventArgs args)
    {

        if (enable && value)
        {
            if (user.IsPlayerGrounded())
            {
                doublejump = true;
            }
            else
            {
                if (doublejump)
                {
                    doublejump = false;
                    user.SetVelocity(Vector3.Scale(user.GetVelocity(), new Vector3(1f, 0f, 1f)) + ((user.GetJumpImpulse() * secondJumpMultiplier) * Vector3.up));
                }
            }

        }


    }
}
