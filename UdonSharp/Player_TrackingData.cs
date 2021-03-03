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
