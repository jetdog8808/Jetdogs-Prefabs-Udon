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
using UnityEngine.AI; //needs to be added to use navagents.
using VRC.SDKBase;
using VRC.Udon;

public class Navagent_Follow : UdonSharpBehaviour
{
    public NavMeshAgent navagent;
    public Transform followTarget;

    void Start()
    {
        if (!navagent)
        {
            navagent = GetComponent<NavMeshAgent>();
        }

    }

    public void Update()
    {
        if (followTarget && followTarget.hasChanged)
        {
            navagent.SetDestination(followTarget.position);
            followTarget.hasChanged = false;
        }
    }
}
