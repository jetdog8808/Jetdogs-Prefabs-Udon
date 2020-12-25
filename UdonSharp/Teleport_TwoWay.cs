
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Teleport_TwoWay : UdonSharpBehaviour
{
    public Transform teleporterPoint;
    public Teleport_TwoWay sendTo;
    [HideInInspector]
    public bool clear = true;

    private void Start()
    {
        if (teleporterPoint)
        {
            teleporterPoint = transform;
        }
    }

    public virtual void OnPlayerTriggerEnter(VRC.SDKBase.VRCPlayerApi player)
    {
        if (sendTo)
        {
            if (player.isLocal && clear)
            {
                sendTo.clear = false;
                player.TeleportTo(sendTo.teleporterPoint.position, sendTo.teleporterPoint.rotation, VRC_SceneDescriptor.SpawnOrientation.AlignPlayerWithSpawnPoint, false);
            }
        }
    }

    public virtual void OnPlayerTriggerExit(VRC.SDKBase.VRCPlayerApi player) 
    {
        if (player.isLocal && !clear)
        {
            clear = true;
        }
    }
}
