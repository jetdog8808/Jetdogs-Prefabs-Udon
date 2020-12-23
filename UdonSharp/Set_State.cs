
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Set_State : UdonSharpBehaviour
{
    public GameObject[] setTrue;
    public GameObject[] setFalse;

    public bool setOnStart = false;
    void Start()
    {
        if (setOnStart)
        {
            SetState();
        }
    }

    public virtual void Interact()
    {
        SetState();
    }

    public void SetState()
    {
        for (int i = 0; i < setTrue.Length; i++)
        {
            setTrue[i].SetActive(true);
        }

        for (int i = 0; i < setFalse.Length; i++)
        {
            setFalse[i].SetActive(true);
        }
    }
}
