
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;

namespace JetDog.Prefabs
{
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
        if (!Utilities.IsValid(other) || other == null)
        {
            Debug.Log("trigger had something bad enter");
            return;
        }

        VRCPickup temppickup = (VRCPickup)other.GetComponent(typeof(VRCPickup));
        

        if (temppickup)
        {
            temppickup.Drop();
            Rigidbody temprigid = other.GetComponent<Rigidbody>();
            if (temprigid) temprigid.velocity = Vector3.zero;
        }

        if (Networking.LocalPlayer.IsOwner(other.gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            pool.Return(other.gameObject);
        }
    }

}
}

