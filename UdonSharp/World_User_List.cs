
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
    public string playerformat = "{Name}|ID:{ID}|{VR}|{Master}";

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
            int usernumber = 0;
            foreach (VRCPlayerApi user in userList)
            {
                if(user != null)
                {
                    usernumber += 1;
                }
            }

            numberDisplay.text = usernumber.ToString();
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
                if (userList[i] != null)
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


                    stringList = string.Concat(stringList, tempstring, "\r\n");
                }
            }

            playerList.text = stringList;
        }
        
        
        
    }
}
