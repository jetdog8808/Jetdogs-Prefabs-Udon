
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
