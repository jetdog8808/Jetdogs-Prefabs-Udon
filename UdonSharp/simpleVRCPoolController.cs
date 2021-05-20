
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
[RequireComponent(typeof(VRCObjectPool)), RequireComponent(typeof(BoxCollider))]
public class simpleVRCPoolController : UdonSharpBehaviour
{
    private VRCObjectPool pool;
    private GameObject returntemp;
    private VRCPickup pickuptemp;

    void Start()
    {
        pool = (VRCObjectPool)GetComponent(typeof(VRCObjectPool));
        GetComponent<BoxCollider>().isTrigger = true;
    }

    public void GetObject()
    {
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        pool.TryToSpawn();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!Utilities.IsValid(other)) //player stations will break this.
        {
            Debug.Log("trigger had something bad enter");
            return;
        }

        VRCPickup temppickup = (VRCPickup)other.GetComponent(typeof(VRCPickup));

        if (temppickup)
        {
            temppickup.Drop();
        }

        if (Networking.LocalPlayer.IsOwner(other.gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            pool.Return(other.gameObject);
        }
    }

}
