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
