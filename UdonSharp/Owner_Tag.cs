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

public class Owner_Tag : UdonSharpBehaviour
{
    public GameObject ownerOf;
    public Transform sign;
    public float offset = 1;
    public bool follow = false;
    public Transform resetpoint;
    private VRCPlayerApi owner;

    void Start()
    {
        if(!ownerOf)
        {
            ownerOf = gameObject;
        }
        if(!sign)
        {
            sign = transform; 
        }
    }

    private void Update()
    {
        if (follow)
        {
            if(!Utilities.IsValid(owner) || !owner.IsOwner(gameObject))
            {
                owner = Networking.GetOwner(gameObject);
            }
            
            sign.SetPositionAndRotation(owner.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position + new Vector3(0, offset, 0), owner.GetRotation());

        }
    }

    public void FollowToggle()
    {
        follow = !follow;

        if (!follow && resetpoint)
        {
            sign.SetPositionAndRotation(resetpoint.position, resetpoint.rotation);
        }
    }
}
