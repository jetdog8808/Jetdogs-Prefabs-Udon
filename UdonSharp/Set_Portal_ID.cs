
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Set_Portal_ID : UdonSharpBehaviour
{
    public VRC_PortalMarker portal;
    public string id;
    public bool networked = true;

    private void Start()
    {
        if (networked && !Networking.IsMaster)
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "LateSync");
        }
    }

    public virtual void Interact()
    {
        if (networked)
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SetID");
        }
        else
        {
            SetID();
        }
        
    }

    public void SetID()
    {
        portal.roomId = id;
    }

    public void LateSync()
    {
        if(portal.roomId == id)
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SetID");
        }
    }
}
