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

public class Teleport_Stick : UdonSharpBehaviour
{
    public Transform rayAim;
    public float distance = Mathf.Infinity;
    public LayerMask hitMask = 2327;

    public Renderer[] rayvisual;
    public Transform hitLocation;
    private bool hitSomething;
    
    private VRC_Pickup pickup;
    private Transform transform;

    void Start()
    {
        Debug.Log(hitMask.value);
        pickup = (VRC_Pickup)GetComponent(typeof(VRC_Pickup));
        transform = GetComponent<Transform>();
        transform.hasChanged = false;

        for (int i = 0; i < rayvisual.Length; i++)
        {
            rayvisual[i].enabled = false;
        }
    }

    private void Update()
    {
        if (pickup.IsHeld && transform.hasChanged)
        {
            RaycastOut();
            transform.hasChanged = false;
        }
        
    }

    public virtual void OnDrop() 
    {
        for (int i = 0; i < rayvisual.Length; i++)
        {
            rayvisual[i].enabled = false;
        }

        hitSomething = false;
    }
   
    public void RaycastOut()
    {
        RaycastHit hitinfotemp;
        if(Physics.Raycast(rayAim.position, rayAim.rotation * Vector3.forward, out hitinfotemp, distance, hitMask, QueryTriggerInteraction.Ignore))
        {
            hitSomething = true;
            hitLocation.SetPositionAndRotation(hitinfotemp.point, Quaternion.FromToRotation(Vector3.up, hitinfotemp.normal));
            for (int i = 0; i < rayvisual.Length; i++)
            {
                if(!rayvisual[i].enabled)
                rayvisual[i].enabled = true;
            }

        }
        else
        {
            hitSomething = false;
            for (int i = 0; i < rayvisual.Length; i++)
            {
                if (rayvisual[i].enabled)
                    rayvisual[i].enabled = false;
            }
            
            
        }
        
    }

    public virtual void OnPickupUseDown() 
    {
        if (hitSomething)
        {
            pickup.currentPlayer.TeleportTo(hitLocation.position, pickup.currentPlayer.GetRotation(), VRC_SceneDescriptor.SpawnOrientation.AlignPlayerWithSpawnPoint, true);
        }
    
    }
}
