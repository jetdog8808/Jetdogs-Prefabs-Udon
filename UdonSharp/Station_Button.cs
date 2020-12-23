
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Station_Button : UdonSharpBehaviour
{
    public VRCStation station;
    [HideInInspector]
    public VRCPlayerApi seated;

    public void Sit()
    {
        if (seated == null)
        {
            station.UseStation(Networking.LocalPlayer);
        }
        else if(seated == Networking.LocalPlayer)
        {
            station.ExitStation(Networking.LocalPlayer);
        }
    }

    public virtual void OnStationEntered(VRC.SDKBase.VRCPlayerApi player) 
    {
        seated = player;
    }
    public virtual void OnStationExited(VRC.SDKBase.VRCPlayerApi player) 
    { 
        if(seated == player)
        {
            seated = null;
        }
    }
}
