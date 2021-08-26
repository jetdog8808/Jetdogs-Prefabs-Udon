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


namespace JetDog.Prefabs
{
public class DateTime_UI : UdonSharpBehaviour
{
    //https://docs.microsoft.com/en-us/dotnet/api/system.globalization.datetimeformatinfo?view=net-5.0#remarks
    [Tooltip("Formatting info in code comments.")]
    public string format = "hh:mm:ss tt";

    [Tooltip("Some ID's in code comments.")]
    public string timeZoneID = string.Empty;
    /*some common timezone ids you can use. 
     * 
     * Pacific Standard Time
     * Mountain Standard Time
     * Central Standard Time
     * Eastern Standard Time
     * GMT Standard Time
     * Central Europe Standard Time
     * Tokyo Standard Time
     * 
     */

    [Space(5)]
    public bool autoupdate = true;

    [Space(5)]
    [Tooltip("Unity UI or TMPro")]
    public MaskableGraphic display;

    private TimeZoneInfo timezone;

    
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
        //checks time each from if autoupdate is true;
        if (!autoupdate) return;

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {

        if (!display)
        {
            return;
        }

        Type type = display.GetType();
        string tempstring = TimeZoneInfo.ConvertTime(DateTime.Now, timezone).ToString(format);

        if (type == typeof(Text))
        {
            if (((Text)display).text != tempstring)
            {
                ((Text)display).text = tempstring;
            }
        }
        else if (type == typeof(TextMeshPro))
        {
            if (((TextMeshPro)display).text != tempstring)
            {
                ((TextMeshPro)display).text = tempstring;
            }
        }
        else if (type == typeof(TextMeshProUGUI))
        {
            if (((TextMeshProUGUI)display).text != tempstring)
            {
                ((TextMeshProUGUI)display).text = tempstring;
            }
        }

    }

}
}

