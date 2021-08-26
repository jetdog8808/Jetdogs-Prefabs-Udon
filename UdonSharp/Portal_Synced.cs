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
    [RequireComponent(typeof(VRC_PortalMarker)), UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class Portal_Synced : UdonSharpBehaviour
    {
        private VRC_PortalMarker portal;
        [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(worldSetID))]
        private string worldID_S;
        private string worldSetID
        {
            set
            {
                worldID_S = value;
                portal.roomId = worldID_S;
                worldID_L = value;

            }
        }


        [FieldChangeCallback(nameof(IDChange))]
        private string worldID_L;
        public string IDChange
        {
            set
            {
                if (!value.Contains("avtr_") && value.Length != 41)
                {
                    return;
                }

                worldSetID = value;

                if (!Networking.IsOwner(gameObject))
                {
                    Networking.SetOwner(Networking.LocalPlayer, gameObject);
                }

                RequestSerialization();

            }

            get => worldID_L;
        }

        void Start()
        {
            portal = (VRC_PortalMarker)GetComponent(typeof(VRC_PortalMarker));
            IDChange = portal.roomId;

        }

    }
}
