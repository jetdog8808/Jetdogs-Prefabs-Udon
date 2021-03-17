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

public class CarStation : UdonSharpBehaviour
{
    public CarControls car;
    public GameObject exitbutton;
    private VRCStation station;
    [HideInInspector]
    public VRCPlayerApi seated;

    void Start()
    {
        station = (VRCStation)GetComponent(typeof(VRCStation));
        exitbutton.SetActive(false);
    }
    public virtual void Interact()
    {

        if (!Utilities.IsValid(seated))
        {
            station.UseStation(Networking.LocalPlayer);
        }
        else if (seated == Networking.LocalPlayer)
        {
            station.ExitStation(Networking.LocalPlayer);
        }
    }

    public virtual void OnStationEntered(VRC.SDKBase.VRCPlayerApi player)
    {
        seated = player;

        if (player.isLocal)
        {
            car.EnteredCar();
            exitbutton.SetActive(true);
        }
        else
        {
            car.RemoteEnteredCar();
        }
    }
    public virtual void OnStationExited(VRC.SDKBase.VRCPlayerApi player)
    {
        if (seated == player)
        {
            seated = null;
        }

        if (player.isLocal)
        {
            car.ExitedCar();
            exitbutton.SetActive(false);
        }
    }

    public void ExitStation()
    {
        station.ExitStation(Networking.LocalPlayer);
    }
}
