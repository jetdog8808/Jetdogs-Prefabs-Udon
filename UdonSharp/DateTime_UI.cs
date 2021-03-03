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

using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI; //needs to be added to interact with unity ui components.
using TMPro;
using VRC.SDKBase;
using VRC.Udon;

public class DateTime_UI : UdonSharpBehaviour
{
    public string timeZoneID = string.Empty;
    public string format = "hh:mm:ss tt";
    public Text uiDisplay;
    public TextMeshPro textMeshDisplay;
    public bool autoupdate = true;
    private TimeZoneInfo timezone;

    /*some common timezone ids you can use. 
     * 
     * Pacific Standard Time
     * Mountain Standard Time
     * Central Standard Time
     * Eastern Standard Time
     * GMT Standard Time
     * Central Europe Standard Time
     * 
     */
    void Start()
    {
        if(timeZoneID == string.Empty)
        {
            timezone = TimeZoneInfo.Local;
        }
        else
        {
            timezone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneID);
        }
    }

    private void Update()
    {
        if (autoupdate)
        {
            if (uiDisplay)
            {
                UIDisplayTime();
            }
            if (textMeshDisplay)
            {
                TextmeshProDisplayTime();
            }
        }
        
    }

    public void UIDisplayTime()
    {
        string tempstring = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timezone).ToString(format);
        if(uiDisplay.text != tempstring)
        {
            uiDisplay.text = tempstring;
        }
        
    }

    public void TextmeshProDisplayTime()
    {
        string tempstring = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timezone).ToString(format);
        if (textMeshDisplay.text != tempstring)
        {
            textMeshDisplay.text = tempstring;
        }
    }
}
