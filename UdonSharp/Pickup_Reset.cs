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
using VRC.SDK3.Components;

public class Pickup_Reset : UdonSharpBehaviour
{
    public VRC_Pickup[] pickup;
    [HideInInspector]
    public VRCObjectSync[] syncCom;
    [HideInInspector]
    public Rigidbody[] pickupRigibody;
    [HideInInspector]
    public Vector3[] originPoint;
    [HideInInspector]
    public Quaternion[] originRotation;
    public bool forceDrop = true;
    
    private void Start()
    {
        syncCom = new VRCObjectSync[pickup.Length];
        originPoint = new Vector3[pickup.Length];
        originRotation = new Quaternion[pickup.Length];
        pickupRigibody = new Rigidbody[pickup.Length];

        for (int i = 0; i < pickup.Length; i++)
        {
            syncCom[i] = (VRCObjectSync)pickup[i].GetComponent(typeof(VRCObjectSync));

            if(syncCom[i] == null)
            {
                pickupRigibody[i] = pickup[i].GetComponent<Rigidbody>();
                originPoint[i] = pickup[i].transform.position;
                originRotation[i] = pickup[i].transform.rotation;
            }
        }
    }
    public virtual void Interact()
    {
        ResetPickup();
    }

    public void ResetPickup()
    {
        for (int i = 0; i < pickup.Length; i++)
        {
            if (syncCom[i] == null)
            {
                if(pickup[i].currentPlayer != null)
                {
                    if (forceDrop)
                    {
                        pickup[i].Drop();
                    }
                    else
                    {
                        continue;
                    }
                }
                
                pickupRigibody[i].position = originPoint[i];
                pickupRigibody[i].rotation = originRotation[i];
            }
            else
            {
                if (pickup[i].currentPlayer != null)
                {
                    if (forceDrop)
                    {
                        pickup[i].Drop(pickup[i].currentPlayer);
                    }
                    else
                    {
                        continue;
                    }
                }
                Networking.SetOwner(Networking.LocalPlayer, syncCom[i].gameObject);
                syncCom[i].Respawn();
            }
        }

    }
   
}
