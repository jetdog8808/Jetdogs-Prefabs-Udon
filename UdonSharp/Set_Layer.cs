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
using UnityEngine.UI; //needs to be added to interact with unity ui components.
using VRC.SDKBase;
using VRC.Udon;

public class Set_Layer : UdonSharpBehaviour
{
    public int layer;
    public Dropdown dropdown;
    public GameObject[] gameobjects;

    public virtual void Interact()
    {
        SetLayer();
    }

    public void SetLayer()
    {
        if (dropdown)
        {
            layer = dropdown.value;
        }

        for (int i = 0; i < gameobjects.Length; i++)
        {
            gameobjects[i].layer = layer;
        }
    }
}
