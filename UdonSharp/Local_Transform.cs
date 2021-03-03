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
