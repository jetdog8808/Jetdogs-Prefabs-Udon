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
using VRC.SDKBase;
using VRC.Udon;

public class Player_Voice_Settings : UdonSharpBehaviour
{
    public bool global = true;
    public Player_Voice_Settings regionRevertSettings;

    [Range(0,24)]
    public float voiceGain = 15;
    [Range(0, 1000000)]
    public float voiceFar = 25;
    [Range(0, 1000000)]
    public float voiceNear = 0;
    [Range(0, 1000)]
    public float voiceVolumetricRadius = 0;
    public bool voiceDisableLowpass = false;

    [Range(0,10)]
    public float avatarMaxGain = 10;
    public float avatarMaxFar = 40;
    public float avatarMaxNear = 40;
    public float avatarMaxVolumetricRadius = 40;
    public bool avatarForceSpatial = false;
    public bool avatarAllowCustomCurve = true;


    public virtual void OnPlayerJoined(VRC.SDKBase.VRCPlayerApi player) 
    {
        if (global)
        {
            SetPlayerAudio(player);
        }
        
    }
    public virtual void OnPlayerTriggerEnter(VRC.SDKBase.VRCPlayerApi player)
    {
        if (!global && regionRevertSettings)
        {
            SetPlayerAudio(player);
        }
    }
    public virtual void OnPlayerTriggerExit(VRC.SDKBase.VRCPlayerApi player)
    {
        if (!global && regionRevertSettings)
        {
            regionRevertSettings.SetPlayerAudio(player);
        }
    }

    public void applytoall() 
    {
        VRCPlayerApi[] players = VRCPlayerApi.GetPlayers(new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()]);
        foreach(VRCPlayerApi player in players)
        {
            SetPlayerAudio(player);
        }
    }

    public void SetPlayerAudio(VRCPlayerApi player)
    {
        if (player.IsValid())
        {
            player.SetVoiceGain(voiceGain);
            player.SetVoiceDistanceFar(voiceFar);
            player.SetVoiceDistanceNear(voiceNear);
            player.SetVoiceVolumetricRadius(voiceVolumetricRadius);
            player.SetVoiceLowpass(!voiceDisableLowpass);

            player.SetAvatarAudioGain(avatarMaxGain);
            player.SetAvatarAudioFarRadius(avatarMaxFar);
            player.SetAvatarAudioNearRadius(avatarMaxNear);
            player.SetAvatarAudioVolumetricRadius(avatarMaxVolumetricRadius);
            player.SetAvatarAudioForceSpatial(avatarForceSpatial);
            player.SetAvatarAudioCustomCurve(avatarAllowCustomCurve);
        }
        
    }

    
}
