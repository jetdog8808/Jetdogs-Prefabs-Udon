
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Join_Bell : UdonSharpBehaviour
{
    public AudioSource bell;
    public bool oneshot = false; 

    void Start()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Joined");
    }

    public void Joined()
    {
        if (oneshot)
        {
            bell.PlayOneShot(bell.clip, bell.volume);
        }
        else
        {
            bell.Play();
        }
    }
}
