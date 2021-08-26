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
    [RequireComponent(typeof(VRC_AvatarPedestal)), UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class Pedestal_Synced : UdonSharpBehaviour
    {
        private VRC_AvatarPedestal pedestal;
        [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(pedestalSetID))]
        private string avatarID_S;
        private string pedestalSetID
        {
            set
            {
                avatarID_S = value;
                pedestal.SwitchAvatar(avatarID_S);
                avatarID_L = value;

            }
        }


        [FieldChangeCallback(nameof(IDChange))]
        private string avatarID_L;
        public string IDChange
        {
            set
            {
                if (!value.Contains("avtr_") && value.Length != 41)
                {
                    return;
                }

                pedestalSetID = value;

                if(!Networking.IsOwner(gameObject))
                {
                    Networking.SetOwner(Networking.LocalPlayer, gameObject);
                }

                RequestSerialization();

            }

            get => avatarID_L;
        }

        void Start()
        {
            pedestal = (VRC_AvatarPedestal)GetComponent(typeof(VRC_AvatarPedestal));
            IDChange = pedestal.blueprintId;

        }

        public override void Interact()
        {
            pedestal.SetAvatarUse(Networking.LocalPlayer);
        }

    }
}
