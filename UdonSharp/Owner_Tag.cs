
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

    void Start()
    {
        if(ownerOf == null)
        {
            ownerOf = gameObject;
        }
        if(sign == null)
        {
            sign = transform; 
        }
    }

    private void Update()
    {
        if (follow)
        {
            VRCPlayerApi owner = Networking.GetOwner(ownerOf);
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
