using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

public class EditableMirror : UdonSharpBehaviour
{
    public VRC_MirrorReflection mirror;

    public bool state = false;
    public bool pixelLights = false;
    public bool occlusion = true;
    public bool localPlayer = true;
    public bool otherPlayers = true;
    public bool pickups = false;
    public bool environment = false;
    public bool ui = false;

    const int localPlayerMask = 0x00040000;
    const int otherPlayerMask = 0x00000200;
    const int uiMask = 0x00001020;
    const int pickupMask = 0x00006000;
    const int alwaysHideMask = 0x000000DE;
    const int otherMask = -292607; //0xFFFB8901
    void Start()
    {
        UpdateMirror();
    }

    public void UpdateMirror()
    {
        mirror.gameObject.SetActive(state);

        mirror.m_DisablePixelLights = !pixelLights;
        mirror.TurnOffMirrorOcclusion = !occlusion;

        int mask = 0;

        if (localPlayer)
        {
            mask |= localPlayerMask;
        }

        if (otherPlayers)
        {
            mask |= otherPlayerMask;
        }

        if (pickups)
        {
            mask |= pickupMask;
        }

        if (environment)
        {
            mask |= otherMask;
        }

        if (ui)
        {
            mask |= uiMask;
        }

        mirror.m_ReflectLayers = mask;
    }

    public void AutoUpdate()
    {
        mirror.OnWillRenderObject();
    }
}
