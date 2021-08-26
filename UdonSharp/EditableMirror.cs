using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using VRC.SDK3.Components;

namespace JetDog.Prefabs
{
[RequireComponent(typeof(VRCMirrorReflection))]
public class EditableMirror : UdonSharpBehaviour
{
    private VRC_MirrorReflection mirror;

    #region settings
    /*properties and variables for mirror settings. each will be applied once the value changes.
     * 
     * U# scripts will do the set property to change settings but,
     * other udonbehaviours will set the private variable to change settings.
     *  
     */

    private int _mask = 0;
    private int Mask
    {
        set
        {
            _mask = value;
            mirror.m_ReflectLayers = _mask;
        }
        get => _mask;
    }
    [SerializeField, FieldChangeCallback(nameof(EnableState))]
    private bool _enableState = false;
    public bool EnableState
    {
        set 
        {
            _enableState = value;
            mirror.gameObject.SetActive(_enableState);
        }

        get => _enableState;
    }
    [SerializeField, FieldChangeCallback(nameof(DisablePixelLights))]
    private bool _disablePixelLights = false;
    public bool DisablePixelLights
    {
        set
        {
            _disablePixelLights = value;
            mirror.m_DisablePixelLights = _disablePixelLights;
        }

        get => _disablePixelLights;
    }
    [SerializeField, FieldChangeCallback(nameof(Occlusion))]
    private bool _occlusion = true;
    public bool Occlusion
    {
        set
        {
            _occlusion = value;
            mirror.TurnOffMirrorOcclusion = !_occlusion;
        }

        get => _occlusion;
    }
    [SerializeField, FieldChangeCallback(nameof(ShowMirrorPlayer))]
    private bool _MirrorPlayer = true;
    public bool ShowMirrorPlayer
    {
        set
        {
            _MirrorPlayer = value;

            if (value)
            {
                Mask = (Mask & ~mirrorPlayerMask) | mirrorPlayerMask;
            }
            else
            {
                Mask = (Mask & ~mirrorPlayerMask);
            }
        }

        get => _MirrorPlayer;
    }
    [SerializeField, FieldChangeCallback(nameof(ShowOtherPlayers))]
    private bool _otherPlayers = true;
    public bool ShowOtherPlayers
    {
        set
        {
            _otherPlayers = value;

            if (value)
            {
                Mask = (Mask & ~otherPlayerMask) | otherPlayerMask;
            }
            else
            {
                Mask = (Mask & ~otherPlayerMask);
            }
        }

        get => _otherPlayers;
    }
    [SerializeField, FieldChangeCallback(nameof(ShowPickups))]
    private bool _pickups = false;
    public bool ShowPickups
    {
        set
        {
            _pickups = value;

            if (value)
            {
                Mask = (Mask & ~pickupMask) | pickupMask;
            }
            else
            {
                Mask = (Mask & ~pickupMask);
            }
        }

        get => _pickups;
    }
    [SerializeField, FieldChangeCallback(nameof(ShowEnvironment))]
    private bool _environment = false;
    public bool ShowEnvironment
    {
        set
        {
            _environment = value;

            if (value)
            {
                Mask = (Mask & ~otherMask) | otherMask;
            }
            else
            {
                Mask = (Mask & ~otherMask);
            }
        }
        get => _environment;
    }
    [SerializeField, FieldChangeCallback(nameof(ShowUI))]
    private bool _ui = false;
    public bool ShowUI
    {
        set
        {
            _ui = value;

            if (value)
            {
                Mask = (Mask & ~uiMask) | uiMask;
            }
            else
            {
                Mask = (Mask & ~uiMask);
            }
        }
        get => _ui;
    }
    #endregion

    #region constants
    //bit mask constants for the cameras culling mask.

    const int mirrorPlayerMask = 0x00_04_00_00; //0b_0000_0000_0000_0100_0000_0000_0000_0000
    const int otherPlayerMask = 0x00_00_02_00; //0b_0000_0000_0000_0000_0000_0010_0000_0000
    const int uiMask = 0x00_00_10_20; //0b_0000_0000_0000_0000_0001_0000_0010_0000
    const int pickupMask = 0x00_00_60_00; //0b_0000_0000_0000_0000_0110_0000_0000_0000
    const int alwaysHideMask = 0x00_00_04_DE; //0b_0000_0000_0000_0000_0000_0100_1101_1110
    const int otherMask = unchecked((int)0xFF_FB_89_01); //0b_1111_1111_1111_1011_1000_1001_0000_0001
    #endregion

    void Start()
    {
        mirror = (VRC_MirrorReflection)GetComponent(typeof(VRC_MirrorReflection));
        SetupFullMirror();
    }
    
    public void SetupFullMirror()
    {
        EnableState = EnableState;
        DisablePixelLights = DisablePixelLights;
        Occlusion = Occlusion;

        ShowMirrorPlayer = ShowMirrorPlayer;
        ShowOtherPlayers = ShowOtherPlayers;
        ShowPickups = ShowPickups;
        ShowEnvironment = ShowEnvironment;
        ShowUI = ShowUI;

    }

    public void HighQuality()
    {
        DisablePixelLights = true;

        ShowMirrorPlayer = true;
        ShowOtherPlayers = true;
        ShowPickups = true;
        ShowEnvironment = true;
    }

    public void LowQuality()
    {
        DisablePixelLights = false;
        Occlusion = true;

        ShowMirrorPlayer = true;
        ShowOtherPlayers = true;
        ShowPickups = false;
        ShowEnvironment = false;
        ShowUI = false;
    }

}
}


