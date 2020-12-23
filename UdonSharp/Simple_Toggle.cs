
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Simple_Toggle : UdonSharpBehaviour
{
    public GameObject[] gameobjectArray;

    public virtual void Interact()
    {
        Toggle();
    }

    public void Toggle()
    {
        for (int i = 0; i < gameobjectArray.Length; i++)
        {
            gameobjectArray[i].SetActive(!gameobjectArray[i].activeSelf);
        }
    }
}
