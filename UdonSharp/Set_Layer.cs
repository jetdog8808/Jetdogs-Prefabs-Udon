
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
