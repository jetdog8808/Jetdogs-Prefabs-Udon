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

public class Fly_Stick : UdonSharpBehaviour
{
    public float speed = 10;
    public Transform flydirection;
    public bool floatInAir = true;

    private VRC_Pickup pickup;
    private bool triggerdown = false;
    private float gravitysave = 1;
    private VRCPlayerApi localUser;

    void Start()
    {
        if (!flydirection)
        {
            flydirection = transform;
        }
        localUser = Networking.LocalPlayer;
        pickup = (VRC_Pickup)GetComponent(typeof(VRC_Pickup));

    }

    private void Update()
    {
        if (pickup.IsHeld)
        {
            if (triggerdown)
            {
                localUser.SetVelocity(flydirection.rotation * Vector3.forward * speed);
                if (floatInAir && localUser.GetGravityStrength() != 0.001f)
                {
                    localUser.SetGravityStrength(0.001f);
                }
            }
            else
            {
                if (floatInAir)
                {
                    if (localUser.IsPlayerGrounded())
                    {
                        if (localUser.GetGravityStrength() != gravitysave)
                        {
                            localUser.SetGravityStrength(gravitysave);
                        }
                    }
                    else
                    {
                        localUser.SetVelocity(Vector3.zero);
                    }
                }
                else
                {
                    if (localUser.GetGravityStrength() != gravitysave)
                    {
                        localUser.SetGravityStrength(gravitysave);
                    }
                }

                
            }
            
        }
    }

    public virtual void OnPickupUseDown() 
    {
        triggerdown = true;
    }
    public virtual void OnPickupUseUp() 
    {
        triggerdown = false;
    }
    public virtual void OnDrop() 
    {
        triggerdown = false;
        localUser.SetGravityStrength(gravitysave);
    }
    public virtual void OnPickup() 
    {
        gravitysave = localUser.GetGravityStrength();
    }
}
