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
public class Set_Float : UdonSharpBehaviour
{
    public UdonBehaviour udonBehaviour; 
    public string variableName;
    public string UpdateMethod;
    [UdonSynced(UdonSyncMode.None)]
    public float value;
    public bool getOnStart = false;
    public bool synce = false;
    public float step = 1f;
    public string format = "0.00";

    public Text display;
    public Slider slider;

    private float originalValue;
    private bool sliderInitialize = false;

    void Start()
    {
        if (getOnStart)
        {
            if (udonBehaviour)
            {
                originalValue = (float)udonBehaviour.GetProgramVariable(variableName);
            }

            value = originalValue;
        }
        else
        {
            originalValue = value;
        }

        if (display)
        {
            display.text = value.ToString(format);
        }

        if (slider)
        {
            slider.value = value;
            sliderInitialize = true;
        }

        if (0 > step)
        {
            step = Mathf.Abs(step);
        }
    }

    public void SetFloat()
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
            if (slider.value != value)
            {
                sliderInitialize = false;
                slider.value = value;
                sliderInitialize = true;
            }
        }

        if (display)
        {
            display.text = value.ToString(format);
        }
    }

    public void Increase()
    {
        value += step;
        SetFloat();
        if (synce)
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            RequestSerialization();
        }
    }

    public void Decrease()
    {
        value -= step;
        SetFloat();
        if (synce)
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            RequestSerialization();
        }
    }

    public void ResetValue()
    {
        value = originalValue;
        SetFloat();
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
            value = slider.value;
            SetFloat();
            if (synce)
            {
                Networking.SetOwner(Networking.LocalPlayer, gameObject);
                RequestSerialization();
            }
        }
    }

    public virtual void OnDeserialization()
    {
        SetFloat();
    }

}
