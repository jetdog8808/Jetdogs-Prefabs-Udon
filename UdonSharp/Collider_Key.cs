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
    public class Collider_Key : UdonSharpBehaviour
    {
        public Collider[] keys;

        public UdonBehaviour udonBehaviour;
        public string eventName;

        //will use this event if the collider is a trigger.
        private void OnTriggerEnter(Collider other)
        {
            //checks if object is not blacklisted. can break if not filtured. 
            if (!Utilities.IsValid(other) || other == null) return;

            //checks key array if object is a key.
            for (int i = 0; i < keys.Length; i++)
            {
                if (other == keys[i])
                {
                    udonBehaviour.SendCustomEvent(eventName);
                    break;
                }
            }
        }

        //will use this event if the collider is not a trigger.
        private void OnCollisionEnter(Collision collision)
        {
            //checks if object is not blacklisted. can break if not filtured. 
            if (!Utilities.IsValid(collision) || collision == null) return;

            //checks key array if object is a key.
            for (int i = 0; i < keys.Length; i++)
            {
                if (collision.collider == keys[i])
                {
                    udonBehaviour.SendCustomEvent(eventName);
                    break;
                }
            }
        }
    }
}

