
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Set_Scale : UdonSharpBehaviour
{
    public Vector3 scale = Vector3.one;
    public Transform s_transform;
    public bool scaleOnStart = true;


    void Start()
    {
        if (!s_transform)
        {
            s_transform = transform;
        }

        if (scaleOnStart)
        {
            SetScale();
        }
    }
    public virtual void Interact() 
    {
        SetScale();
    }

    public void SetScale()
    {
        s_transform.localScale = scale;
    }
}
