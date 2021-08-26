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
using System;

namespace JetDog.Prefabs
{
public class World_User_List : UdonSharpBehaviour
{
    [HideInInspector]
    public VRCPlayerApi[] userList = new VRCPlayerApi[80];
    public Text numberDisplay;
    public Text playerList;
    [Tooltip("user display name = {Name}\nNetwork ID = {ID}\nVR or Desktop = {VR}\nMaster of instance = {Master}\nInstance Owner = {Instance}")]
    public string playerformat = "{Name}|ID:{ID}|{VR}|{Master}|{Instance}";

    private void Start()
    {
        playerformat = playerformat
            .Replace("{Name}", "{0}")
            .Replace("{ID}", "{1}")
            .Replace("{VR}", "{2}")
            .Replace("{Master}", "{3}")
            .Replace("{Instance}", "{4}");
        Debug.Log(playerformat);
    }

    public virtual void OnPlayerJoined(VRC.SDKBase.VRCPlayerApi player) 
    {
        RefreshList();
    }

    public virtual void OnPlayerLeft(VRC.SDKBase.VRCPlayerApi player) 
    {
        SendCustomEventDelayedFrames(nameof(RefreshList), 1, VRC.Udon.Common.Enums.EventTiming.Update);
    }

    public void RefreshList()
    {
        Array.Clear(userList, 0, userList.Length);

        userList = VRCPlayerApi.GetPlayers(userList);

        RefreshUsers();
        RefreshCount();
    }

    public void RefreshUsers()
    {
        if (!playerList) return;

        string stringList = string.Empty;

        for (int i = 0; i < userList.Length; i++)
        {
            if (userList[i] == null || !Utilities.IsValid(userList[i])) continue;


            /*
             * {0} = display name
             * {1} = network id
             * {2} = vr mode
             * {3} = instance master
             * {4} = instance owner
             */
            stringList = string.Concat(stringList, string.Format(playerformat, userList[i].displayName, userList[i].playerId, userList[i].IsUserInVR() ? "VR" : "Desktop", userList[i].isMaster ? "Master" : string.Empty, userList[i].isInstanceOwner ? "Instance Owner" : string.Empty), "\r\n");
        }

        playerList.text = stringList;

    }

    public void RefreshCount()
    {
        if (numberDisplay)
        {
            numberDisplay.text = VRCPlayerApi.GetPlayerCount().ToString();
        }
    }
}
}

