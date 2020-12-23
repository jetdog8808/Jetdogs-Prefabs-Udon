
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Trigger_Teleporter : UdonSharpBehaviour
{
    public Transform teleportPoint;
    public VRC_SceneDescriptor.SpawnOrientation teleportOrientation = VRC_SceneDescriptor.SpawnOrientation.AlignPlayerWithSpawnPoint;
    public bool lerpOnRemote = false;

    public virtual void OnPlayerTriggerEnter(VRC.SDKBase.VRCPlayerApi player) 
    {
        if (player.isLocal)
        {
            player.TeleportTo(teleportPoint.position, teleportPoint.rotation, teleportOrientation, lerpOnRemote);
        }
    }
}
