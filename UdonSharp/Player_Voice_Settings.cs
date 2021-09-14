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

namespace JetDog.Prefabs
{
public class Player_Voice_Settings : UdonSharpBehaviour
{
    [SerializeField]
    private bool global = true;

    #region properties & variables
    [SerializeField,Range(0,24)]
    private float _voiceGain = 15;
    public float SetVoiceGain
    {
        set
        {
            _voiceGain = Mathf.Clamp(value, 0, 24);
            UpdateUserArray();

            if (global)
            {
                user.SetPlayerTag(voiceGain_S, _voiceGain.ToString());

                foreach(VRCPlayerApi p in allUsers)
                {
                    if(p.GetPlayerTag(inRegion) == null)
                    {
                        p.SetVoiceGain(_voiceGain);
                    }
                }
            }
            else
            {

                foreach (VRCPlayerApi p in allUsers)
                {
                    if (!int.TryParse(p.GetPlayerTag(inRegion), out id))
                    {
                        continue;
                    }

                    if(id == gameObject.GetInstanceID())
                    {
                        p.SetVoiceGain(_voiceGain);
                    }
                }
            }
            
        }

        get => _voiceGain;
    }

    [SerializeField,Range(0, 1000000)]
    private float _voiceFar = 25;
    public float SetVoicefar
    {
        set
        {
            _voiceFar = Mathf.Clamp(value, 0, 1000000);
            UpdateUserArray();

            if (global)
            {
                user.SetPlayerTag(voiceFar_S, _voiceFar.ToString());

                foreach (VRCPlayerApi p in allUsers)
                {
                    if (p.GetPlayerTag(inRegion) == null)
                    {
                        p.SetVoiceDistanceFar(_voiceFar);
                    }
                }
            }
            else
            {

                foreach (VRCPlayerApi p in allUsers)
                {
                    if (!int.TryParse(p.GetPlayerTag(inRegion), out id))
                    {
                        continue;
                    }

                    if (id == gameObject.GetInstanceID())
                    {
                        p.SetVoiceDistanceFar(_voiceFar);
                    }
                }
            }

        }

        get => _voiceFar;
    }

    [SerializeField,Range(0, 1000000)]
    private float _voiceNear = 0;
    public float SetVoiceNear
    {
        set
        {
            _voiceNear = Mathf.Clamp(value, 0, 1000000);
            UpdateUserArray();

            if (global)
            {
                user.SetPlayerTag(voiceNear_S, _voiceNear.ToString());

                foreach (VRCPlayerApi p in allUsers)
                {
                    if (p.GetPlayerTag(inRegion) == null)
                    {
                        p.SetVoiceDistanceNear(_voiceNear);
                    }
                }
            }
            else
            {

                foreach (VRCPlayerApi p in allUsers)
                {
                    if (!int.TryParse(p.GetPlayerTag(inRegion), out id))
                    {
                        continue;
                    }

                    if (id == gameObject.GetInstanceID())
                    {
                        p.SetVoiceDistanceNear(_voiceNear);
                    }
                }
            }

        }

        get => _voiceNear;
    }

    [SerializeField,Range(0, 1000)]
    private float _voiceVolumetricRadius = 0;
    public float SetVoiceVolumetricRadius
    {
        set
        {
            _voiceVolumetricRadius = Mathf.Clamp(value, 0, 1000);
            UpdateUserArray();

            if (global)
            {
                user.SetPlayerTag(voiceVolumetricRadius_S, _voiceVolumetricRadius.ToString());

                foreach (VRCPlayerApi p in allUsers)
                {
                    if (p.GetPlayerTag(inRegion) == null)
                    {
                        p.SetVoiceVolumetricRadius(_voiceVolumetricRadius);
                    }
                }
            }
            else
            {

                foreach (VRCPlayerApi p in allUsers)
                {
                    if (!int.TryParse(p.GetPlayerTag(inRegion), out id))
                    {
                        continue;
                    }

                    if (id == gameObject.GetInstanceID())
                    {
                        p.SetVoiceVolumetricRadius(_voiceVolumetricRadius);
                    }
                }
            }

        }

        get => _voiceVolumetricRadius;
    }

    [SerializeField]
    private bool _voiceLowpass = true;
    public bool SetVoiceLowpass
    {
        set
        {
            _voiceLowpass = value;
            UpdateUserArray();

            if (global)
            {
                user.SetPlayerTag(voiceLowpass_S, _voiceLowpass.ToString());

                foreach (VRCPlayerApi p in allUsers)
                {
                    if (p.GetPlayerTag(inRegion) == null)
                    {
                        p.SetVoiceLowpass(_voiceLowpass);
                    }
                }
            }
            else
            {

                foreach (VRCPlayerApi p in allUsers)
                {
                    if (!int.TryParse(p.GetPlayerTag(inRegion), out id))
                    {
                        continue;
                    }

                    if (id == gameObject.GetInstanceID())
                    {
                        p.SetVoiceLowpass(_voiceLowpass);
                    }
                }
            }

        }

        get => _voiceLowpass;
    }

    [SerializeField,Range(0,10)]
    private float _avatarMaxGain = 10;
    public float SetAvatarMaxGain
    {
        set
        {
            _avatarMaxGain = Mathf.Clamp(value, 0, 10);
            UpdateUserArray();

            if (global)
            {
                user.SetPlayerTag(avatarMaxGain_S, _avatarMaxGain.ToString());

                foreach (VRCPlayerApi p in allUsers)
                {
                    if (p.GetPlayerTag(inRegion) == null)
                    {
                        p.SetAvatarAudioGain(_avatarMaxGain);
                    }
                }
            }
            else
            {

                foreach (VRCPlayerApi p in allUsers)
                {
                    if (!int.TryParse(p.GetPlayerTag(inRegion), out id))
                    {
                        continue;
                    }

                    if (id == gameObject.GetInstanceID())
                    {
                        p.SetAvatarAudioGain(_avatarMaxGain);
                    }
                }
            }

        }

        get => _avatarMaxGain;
    }

    [SerializeField]
    private float _avatarMaxFar = 40;
    public float SetAvatarMaxFar
    {
        set
        {
            _avatarMaxFar = value;
            UpdateUserArray();

            if (global)
            {
                user.SetPlayerTag(avatarMaxFar_S, _avatarMaxFar.ToString());

                foreach (VRCPlayerApi p in allUsers)
                {
                    if (p.GetPlayerTag(inRegion) == null)
                    {
                        p.SetAvatarAudioFarRadius(_avatarMaxFar);
                    }
                }
            }
            else
            {

                foreach (VRCPlayerApi p in allUsers)
                {
                    if (!int.TryParse(p.GetPlayerTag(inRegion), out id))
                    {
                        continue;
                    }

                    if (id == gameObject.GetInstanceID())
                    {
                        p.SetAvatarAudioFarRadius(_avatarMaxFar);
                    }
                }
            }

        }

        get => _avatarMaxFar;
    }

    [SerializeField]
    private float _avatarMaxNear = 40;
    public float SetAvatarMaxNear
    {
        set
        {
            _avatarMaxNear = value;
            UpdateUserArray();

            if (global)
            {
                user.SetPlayerTag(avatarMaxNear_S, _avatarMaxNear.ToString());

                foreach (VRCPlayerApi p in allUsers)
                {
                    if (p.GetPlayerTag(inRegion) == null)
                    {
                        p.SetAvatarAudioNearRadius(_avatarMaxNear);
                    }
                }
            }
            else
            {

                foreach (VRCPlayerApi p in allUsers)
                {
                    if (!int.TryParse(p.GetPlayerTag(inRegion), out id))
                    {
                        continue;
                    }

                    if (id == gameObject.GetInstanceID())
                    {
                        p.SetAvatarAudioNearRadius(_avatarMaxNear);
                    }
                }
            }

        }

        get => _avatarMaxNear;
    }

    [SerializeField]
    private float _avatarMaxVolumetricRadius = 40;
    public float SetAvatarVolumetricRadius
    {
        set
        {
            _avatarMaxVolumetricRadius = value;
            UpdateUserArray();

            if (global)
            {
                user.SetPlayerTag(avatarMaxVolumetricRadius_S, _avatarMaxVolumetricRadius.ToString());

                foreach (VRCPlayerApi p in allUsers)
                {
                    if (p.GetPlayerTag(inRegion) == null)
                    {
                        p.SetAvatarAudioVolumetricRadius(_avatarMaxVolumetricRadius);
                    }
                }
            }
            else
            {

                foreach (VRCPlayerApi p in allUsers)
                {
                    if (!int.TryParse(p.GetPlayerTag(inRegion), out id))
                    {
                        continue;
                    }

                    if (id == gameObject.GetInstanceID())
                    {
                        p.SetAvatarAudioVolumetricRadius(_avatarMaxVolumetricRadius);
                    }
                }
            }

        }

        get => _avatarMaxVolumetricRadius;
    }

    [SerializeField]
    private bool _avatarForceSpatial = false;
    public bool SetAvatarForceSpatial
    {
        set
        {
            _avatarForceSpatial = value;
            UpdateUserArray();

            if (global)
            {
                user.SetPlayerTag(avatarForceSpatial_S, _avatarForceSpatial.ToString());

                foreach (VRCPlayerApi p in allUsers)
                {
                    if (p.GetPlayerTag(inRegion) == null)
                    {
                        p.SetAvatarAudioForceSpatial(_avatarForceSpatial);
                    }
                }
            }
            else
            {

                foreach (VRCPlayerApi p in allUsers)
                {
                    if (!int.TryParse(p.GetPlayerTag(inRegion), out id))
                    {
                        continue;
                    }

                    if (id == gameObject.GetInstanceID())
                    {
                        p.SetAvatarAudioForceSpatial(_avatarForceSpatial);
                    }
                }
            }

        }

        get => _avatarForceSpatial;
    }

    [SerializeField]
    private bool _avatarAllowCustomCurve = true;
    public bool SetAvatarAllowCustomCurve
    {
        set
        {
            _avatarAllowCustomCurve = value;
            UpdateUserArray();

            if (global)
            {
                user.SetPlayerTag(avatarAllowCustomCurve_S, _avatarAllowCustomCurve.ToString());

                foreach (VRCPlayerApi p in allUsers)
                {
                    if (p.GetPlayerTag(inRegion) == null)
                    {
                        p.SetAvatarAudioCustomCurve(_avatarAllowCustomCurve);
                    }
                }
            }
            else
            {

                foreach (VRCPlayerApi p in allUsers)
                {
                    if (!int.TryParse(p.GetPlayerTag(inRegion), out id))
                    {
                        continue;
                    }

                    if (id == gameObject.GetInstanceID())
                    {
                        p.SetAvatarAudioCustomCurve(_avatarAllowCustomCurve);
                    }
                }
            }

        }

        get => _avatarAllowCustomCurve;
    }

    #endregion


    private VRCPlayerApi user;
    private VRCPlayerApi[] allUsers;
    private int id; //small optimization

    #region string constants

    const string instanceid = "InstID_JD";
    const string inRegion = "RegionID_JD";

    const string voiceGain_S = "V_Gain_JD";
    const string voiceFar_S = "V_Far_JD";
    const string voiceNear_S = "V_Near_JD";
    const string voiceVolumetricRadius_S = "V_VolRad_JD";
    const string voiceLowpass_S = "V_DisLowPass_JD";
    const string avatarMaxGain_S = "A_Gain_JD";
    const string avatarMaxFar_S = "A_Far_JD";
    const string avatarMaxNear_S = "A_Near_JD";
    const string avatarMaxVolumetricRadius_S = "A_VolRad_JD";
    const string avatarForceSpatial_S = "A_ForceSpatial_JD";
    const string avatarAllowCustomCurve_S = "A_AllowCurce_JD";

    #endregion

    public void Start()
    {
        user = Networking.LocalPlayer;

        if (!global)
        {
            return;
        }

        if(user.GetPlayerTag(instanceid) == null)
        {
            user.SetPlayerTag(instanceid, gameObject.GetInstanceID().ToString());

            SetupTags();
        }
        else
        {
            global = false;
        }
    }

    private void UpdateUserArray()
    {
        if (allUsers.Length == VRCPlayerApi.GetPlayerCount())
        {
            allUsers = VRCPlayerApi.GetPlayers(allUsers);
        }
        else
        {
            allUsers = VRCPlayerApi.GetPlayers(new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()]);
        }
    }

    public virtual void OnPlayerJoined(VRC.SDKBase.VRCPlayerApi player) 
    {
        if (global)
        {
            SetPlayerAudio(player);
        }
        
    }
    public virtual void OnPlayerTriggerEnter(VRC.SDKBase.VRCPlayerApi player)
    {
        if (!global && player.GetPlayerTag(inRegion) == null)
        {
            player.SetPlayerTag(inRegion, gameObject.GetInstanceID().ToString());
            SetPlayerAudio(player);
        }
    }
    public virtual void OnPlayerTriggerExit(VRC.SDKBase.VRCPlayerApi player)
    {
        if (!global && int.TryParse(player.GetPlayerTag(inRegion), out id))
        {
            if(id == gameObject.GetInstanceID())
            {
                RevertAudio(player);
            }
        }
    }
    /*
    public void applytoall() 
    {
        VRCPlayerApi[] players = VRCPlayerApi.GetPlayers(new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()]);
        foreach(VRCPlayerApi player in players)
        {
            SetPlayerAudio(player);
        }
    }*/

    public void SetupTags()
    {

        user.SetPlayerTag(voiceGain_S, _voiceGain.ToString());
        user.SetPlayerTag(voiceFar_S, _voiceFar.ToString());
        user.SetPlayerTag(voiceNear_S, _voiceNear.ToString());
        user.SetPlayerTag(voiceVolumetricRadius_S, _voiceVolumetricRadius.ToString());
        user.SetPlayerTag(voiceLowpass_S, _voiceLowpass.ToString());

        user.SetPlayerTag(avatarMaxGain_S, _avatarMaxGain.ToString());
        user.SetPlayerTag(avatarMaxFar_S, _avatarMaxFar.ToString());
        user.SetPlayerTag(avatarMaxNear_S, _avatarMaxNear.ToString());
        user.SetPlayerTag(avatarMaxVolumetricRadius_S, _avatarMaxVolumetricRadius.ToString());
        user.SetPlayerTag(avatarForceSpatial_S, _avatarForceSpatial.ToString());
        user.SetPlayerTag(avatarAllowCustomCurve_S, _avatarAllowCustomCurve.ToString());

    }

    public void SetPlayerAudio(VRCPlayerApi player)
    {
        if (!Utilities.IsValid(player) || player == null)
        {
            return;
        }

        player.SetVoiceGain(_voiceGain);
        player.SetVoiceDistanceFar(_voiceFar);
        player.SetVoiceDistanceNear(_voiceNear);
        player.SetVoiceVolumetricRadius(_voiceVolumetricRadius);
        player.SetVoiceLowpass(_voiceLowpass);

        player.SetAvatarAudioGain(_avatarMaxGain);
        player.SetAvatarAudioFarRadius(_avatarMaxFar);
        player.SetAvatarAudioNearRadius(_avatarMaxNear);
        player.SetAvatarAudioVolumetricRadius(_avatarMaxVolumetricRadius);
        player.SetAvatarAudioForceSpatial(_avatarForceSpatial);
        player.SetAvatarAudioCustomCurve(_avatarAllowCustomCurve);

    }

    public void RevertAudio(VRCPlayerApi player)
    {
        if (!Utilities.IsValid(player) || player == null || user.GetPlayerTag(instanceid) == null)
        {
            return;
        }

        player.SetVoiceGain(int.Parse(user.GetPlayerTag(voiceGain_S)));
        player.SetVoiceDistanceFar(int.Parse(user.GetPlayerTag(voiceFar_S)));
        player.SetVoiceDistanceNear(int.Parse(user.GetPlayerTag(voiceNear_S)));
        player.SetVoiceVolumetricRadius(int.Parse(user.GetPlayerTag(voiceVolumetricRadius_S)));
        player.SetVoiceLowpass(bool.Parse(user.GetPlayerTag(voiceLowpass_S)));

        player.SetAvatarAudioGain(int.Parse(user.GetPlayerTag(avatarMaxGain_S)));
        player.SetAvatarAudioFarRadius(int.Parse(user.GetPlayerTag(avatarMaxFar_S)));
        player.SetAvatarAudioNearRadius(int.Parse(user.GetPlayerTag(avatarMaxNear_S)));
        player.SetAvatarAudioVolumetricRadius(int.Parse(user.GetPlayerTag(avatarMaxVolumetricRadius_S)));
        player.SetAvatarAudioForceSpatial(bool.Parse(user.GetPlayerTag(avatarForceSpatial_S)));
        player.SetAvatarAudioCustomCurve(bool.Parse(user.GetPlayerTag(avatarAllowCustomCurve_S)));

        player.SetPlayerTag(inRegion, null);
    }

}
}

