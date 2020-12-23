
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Pickup_Reset : UdonSharpBehaviour
{
    public VRC_Pickup pickup;
    public Transform resetPoint;
    public bool forceDrop = true;
    [HideInInspector]
    public Rigidbody pickupRigidody;
    private void Start()
    {
        pickupRigidody = pickup.GetComponent<Rigidbody>();
    }
    public virtual void Interact()
    {
        ResetPickup();
    }

    public void ResetPickup()
    {
        if(pickup.IsHeld)
        {
            if (forceDrop)
            {
                if (pickup.currentPlayer.isLocal)
                {
                    DropandReset();
                }
                else
                {
                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "DropandReset");
                }
            }
        }
        else
        {
            Networking.SetOwner(Networking.LocalPlayer, pickup.gameObject);

            pickupRigidody.position = resetPoint.position;
            pickupRigidody.rotation = resetPoint.rotation;
        }
    }

    public void DropandReset()
    {
        if (pickup.currentPlayer.isLocal)
        {
            pickup.Drop();
            pickupRigidody.position = resetPoint.position;
            pickupRigidody.rotation = resetPoint.rotation;
        }
    }
   
}
