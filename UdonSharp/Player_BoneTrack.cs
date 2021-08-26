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

namespace JetDog.Prefabs
{
public class Player_BoneTrack : UdonSharpBehaviour
{
    public Transform transform;
    public bool track = true;
    public HumanBodyBones Bone;
    private VRCPlayerApi user;



    void Start()
    {
        if (transform == null)
        {
            transform = GetComponent<Transform>();
        }

        user = Networking.LocalPlayer;

    }

    private void Update()
    {
        if (track)
        {
            transform.SetPositionAndRotation(user.GetBonePosition(Bone), user.GetBoneRotation(Bone));
        }
        
    }
}
}

