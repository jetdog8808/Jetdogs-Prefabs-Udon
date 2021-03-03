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
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class Random_Int : UdonSharpBehaviour
{
    public int min;
    public int max;
    [HideInInspector]
    public int result;
    public Text display;

    public virtual void Interact() 
    {
        GenerateInt();
    }

    public void GenerateInt()
    {
        result = Random.Range(min, max);

        if (display)
        {
            display.text = result.ToString();
        }
    }
}
