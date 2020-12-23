
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Set_Material : UdonSharpBehaviour
{
    public Material material;
    public Renderer[] renderers;
    public bool sharedMaterial = true;

    public virtual void Interact()
    {
        SetMaterial();
    }

    public void SetMaterial()
    {
        foreach(Renderer mesh in renderers)
        {
            if (sharedMaterial)
            {
                mesh.sharedMaterial = material;
            }
            else
            {
                mesh.material = material;
            }
        }
    }
}
