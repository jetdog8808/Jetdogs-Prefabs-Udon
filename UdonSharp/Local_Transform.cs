
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Local_Transform : UdonSharpBehaviour
{
    public Transform transform;
    public Transform referenceTransform;

    void Start()
    {
        if (!transform)
        {
            transform = GetComponent<Transform>();
        }
    }

    private void Update()
    {
        transform.localPosition = referenceTransform.localPosition;
        transform.localRotation = referenceTransform.localRotation;
    }
}
