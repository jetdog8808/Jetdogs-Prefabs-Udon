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
