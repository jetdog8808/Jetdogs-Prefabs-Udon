
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
