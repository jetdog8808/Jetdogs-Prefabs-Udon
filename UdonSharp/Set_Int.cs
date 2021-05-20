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

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class Set_Int : UdonSharpBehaviour
{
    public UdonBehaviour udonBehaviour;
    public string variableName;
    public string UpdateMethod;
    [UdonSynced(UdonSyncMode.None)]
    public int value;
    public bool getOnStart = false;
    public bool synce = false;
    public int step = 1;

    public Text display;
    public Slider slider;

    private int originalValue;
    private bool sliderInitialize = false;

    void Start()
    {
        if (getOnStart)
        {
            if (udonBehaviour)
            {
                originalValue = (int)udonBehaviour.GetProgramVariable(variableName);
            }
            
            value = originalValue;
        }
        else
        {
            originalValue = value;
        }

        if (display)
        {
            display.text = value.ToString();
        }

        if (slider)
        {
            slider.wholeNumbers = true;
            slider.value = value;
            sliderInitialize = true;
        }

        if (0 > step)
        {
            step = Mathf.Abs(step);
        }
    }

    public void SetInt()
    {
        if (udonBehaviour)
        {
            udonBehaviour.SetProgramVariable(variableName, value);
            if (UpdateMethod != string.Empty)
            {
                udonBehaviour.SendCustomEvent(UpdateMethod);
            }
        }

        if (slider)
        {
            if(slider.value != value)
            {
                sliderInitialize = false;
                slider.value = value;
                sliderInitialize = true;
            }
        }

        if (display)
        {
            display.text = value.ToString();
        }
    }

    public void Increase()
    {
        value += step;
        SetInt();
        if (synce)
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            RequestSerialization();
        }
    }

    public void Decrease()
    {
        value -= step;
        SetInt();
        if (synce)
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            RequestSerialization();
        }
    }

    public void ResetValue()
    {
        value = originalValue;
        SetInt();
        if (synce)
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            RequestSerialization();
        }
    }

    public void SetSlider()
    {
        if (slider && sliderInitialize)
        {
            value = Mathf.RoundToInt(slider.value);
            SetInt();
            if (synce)
            {
                Networking.SetOwner(Networking.LocalPlayer, gameObject);
                RequestSerialization();
            }
        }
    }
   
    public virtual void OnDeserialization()
    {
        SetInt();
    }

}
