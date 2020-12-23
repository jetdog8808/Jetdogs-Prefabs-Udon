
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Player_TrackingData : UdonSharpBehaviour
{
    public Transform transform;
    public bool track = true;
    public VRCPlayerApi.TrackingDataType trackingDataType;

    void Start()
    {
        if(transform == null)
        {
            transform = GetComponent<Transform>();
        }    
    }

    private void Update()
    {
        if (track)
        {
            VRCPlayerApi.TrackingData trackdata = Networking.LocalPlayer.GetTrackingData(trackingDataType);
            transform.SetPositionAndRotation(trackdata.position, trackdata.rotation);
        }
        
    }
}
