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

namespace JetDog
{
    [UdonBehaviourSyncMode( BehaviourSyncMode.Manual)]
    public class Variable_Synce_Toggle : UdonSharpBehaviour
    {
        public GameObject[] gameobjectArray;
        [UdonSynced(UdonSyncMode.None)]
        private bool syncState = false;
        private bool state = false;

        public virtual void Interact() 
        {
            SyncToggle();
        }

        public void SyncToggle()
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            Toggle();
        }

        public void Toggle()
        {
            state = !state;
            
            for (int i = 0; i < gameobjectArray.Length; i++)
            {
                gameobjectArray[i].SetActive(!gameobjectArray[i].activeSelf);
            }

            if (Networking.LocalPlayer.IsOwner(gameObject))
            {
                syncState = state;
                RequestSerialization();
            }
        }

        public virtual void OnDeserialization() 
        { 
            if(state != syncState)
            {
                Toggle();
            }
        }

      
    }
}
