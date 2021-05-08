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

public class Set_AvatarPedestal_ID : UdonSharpBehaviour
{
    public VRC_AvatarPedestal pedestal;
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
        pedestal.blueprintId = id;
    }

    public void LateSync()
    {
        if (pedestal.blueprintId == id)
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SetID");

        }
    }
}