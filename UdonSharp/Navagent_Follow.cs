
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
        if (followTarget)
        {
            if (followTarget.hasChanged)
            {
                navagent.SetDestination(followTarget.position);
                followTarget.hasChanged = false;
            }
        }
    }
}
