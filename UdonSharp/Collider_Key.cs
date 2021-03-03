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

public class Collider_Key : UdonSharpBehaviour
{
    public Collider[] keys;

    public UdonBehaviour udonBehaviour; 
    public string eventName;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if(other == keys[i])
            {
                udonBehaviour.SendCustomEvent(eventName);
                break;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
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
