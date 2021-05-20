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

public class World_User_List : UdonSharpBehaviour
{
    [HideInInspector]
    public VRCPlayerApi[] userList;
    public Text numberDisplay;
    public Text playerList;
    public string playerformat = "{Name}|ID:{ID}|{VR}|{Master}|{Instance Owner}";

    public virtual void OnPlayerJoined(VRC.SDKBase.VRCPlayerApi player) 
    {
        RefreshList();
        RefreshUsers();

        if (numberDisplay)
        {
            numberDisplay.text = VRCPlayerApi.GetPlayerCount().ToString();
        }
    }
    public virtual void OnPlayerLeft(VRC.SDKBase.VRCPlayerApi player) //getting the player array on player leave still has the leaving player so you must remove it manually.
    {
        
        for (int i = 0; i < userList.Length; i++)
        {
            if(userList[i] == player)
            {
                userList[i] = null;
                break;
            }
        }
        RefreshUsers();

        if (numberDisplay)
        {
            /*
            int usernumber = 0;
            foreach (VRCPlayerApi user in userList)
            {
                if(user != null)
                {
                    usernumber += 1;
                }
            }

            numberDisplay.text = usernumber.ToString();
            */
            numberDisplay.text = VRCPlayerApi.GetPlayerCount().ToString();
        }
    }

    public void RefreshList()
    {
        userList = VRCPlayerApi.GetPlayers(new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()]);
    }

    public void RefreshUsers()
    {
        if (playerList)
        {
            string stringList = string.Empty;

            for (int i = 0; i < userList.Length; i++)
            {
                if (Utilities.IsValid(userList[i]))
                {
                    string tempstring = playerformat.Replace("{Name}", userList[i].displayName);
                    tempstring = tempstring.Replace("{ID}", userList[i].playerId.ToString());
                    if (userList[i].IsUserInVR())
                    {
                        tempstring = tempstring.Replace("{VR}", "VR");
                    }
                    else
                    {
                        tempstring = tempstring.Replace("{VR}", "Desktop");
                    }
                    if (userList[i].isMaster)
                    {
                        tempstring = tempstring.Replace("{Master}", "Master");
                    }
                    else
                    {
                        tempstring = tempstring.Replace("{Master}", string.Empty);
                    }
                    if (userList[i].isInstanceOwner)
                    {
                        tempstring = tempstring.Replace("{Instance Owner}", "Instance Owner");
                    }
                    else
                    {
                        tempstring = tempstring.Replace("{Instance Owner}", string.Empty);
                    }

                    stringList = string.Concat(stringList, tempstring, "\r\n");
                }
            }

            playerList.text = stringList;
        }
        
        
        
    }
}
