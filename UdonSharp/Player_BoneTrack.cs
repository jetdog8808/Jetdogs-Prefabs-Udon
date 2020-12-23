
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Player_BoneTrack : UdonSharpBehaviour
{
    public Transform transform;
    public bool track = true;
    public HumanBodyBones Bone;

    void Start()
    {
        if (transform == null)
        {
            transform = GetComponent<Transform>();
        }
    }

    private void Update()
    {
        VRCPlayerApi player = Networking.LocalPlayer;
        transform.SetPositionAndRotation(player.GetBonePosition(Bone), player.GetBoneRotation(Bone));
    }
}
