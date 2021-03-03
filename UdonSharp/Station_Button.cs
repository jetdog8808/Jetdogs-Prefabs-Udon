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

public class Station_Button : UdonSharpBehaviour
{
    public VRCStation station;
    [HideInInspector]
    public VRCPlayerApi seated;

    public void Sit()
    {
        if (!Utilities.IsValid(seated))
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
