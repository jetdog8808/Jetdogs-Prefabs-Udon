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
public class Player_Movement_settings : UdonSharpBehaviour
{
    #region properties&variables

    [SerializeField, FieldChangeCallback(nameof(JumpValue))]
    private float _jumpImpulse = 3;
    public float JumpValue
    {
        set
        {
            user.SetJumpImpulse(value);
            _jumpImpulse = user.GetJumpImpulse();
        }

        get
        {
            if(_jumpImpulse != user.GetJumpImpulse())
            {
                _jumpImpulse = user.GetJumpImpulse();
            }

            return _jumpImpulse;
        }
    }

    [SerializeField, FieldChangeCallback(nameof(WalkValue))]
    private float _walkSpeed = 2;
    public float WalkValue
    {
        set
        {
            user.SetWalkSpeed(value);
            _walkSpeed = user.GetWalkSpeed();
        }

        get
        {
            if (_walkSpeed != user.GetWalkSpeed())
            {
                _walkSpeed = user.GetWalkSpeed();
            }

            return _walkSpeed;
        }
    }

    [SerializeField, FieldChangeCallback(nameof(StrafeValue))]
    private float _strafeSpeed = 2;
    public float StrafeValue
    {
        set
        {
            user.SetStrafeSpeed(value);
            _strafeSpeed = user.GetStrafeSpeed();
        }

        get
        {
            if (_strafeSpeed != user.GetStrafeSpeed())
            {
                _strafeSpeed = user.GetStrafeSpeed();
            }

            return _strafeSpeed;
        }
    }

    [SerializeField, FieldChangeCallback(nameof(RunValue))]
    private float _runSpeed = 4;
    public float RunValue
    {
        set
        {
            user.SetRunSpeed(value);
            _runSpeed = user.GetRunSpeed();
        }

        get
        {
            if (_runSpeed != user.GetRunSpeed())
            {
                _runSpeed = user.GetRunSpeed();
            }

            return _runSpeed;
        }
    }

    [SerializeField, FieldChangeCallback(nameof(GravityValue))]
    private float _gravityStrength = 1;
    public float GravityValue
    {
        set
        {
            user.SetGravityStrength(value);
            _gravityStrength = user.GetGravityStrength();
        }

        get
        {
            if (_gravityStrength != user.GetGravityStrength())
            {
                _gravityStrength = user.GetGravityStrength();
            }

            return _gravityStrength;
        }
    }

    [SerializeField, FieldChangeCallback(nameof(LegacyLocomotion))]
    private bool _legacyLocomotion = false;
    public bool LegacyLocomotion
    {
        set
        {
            if (value || _legacyLocomotion)
            {
                user.UseLegacyLocomotion();
                _legacyLocomotion = true;
            }
            else
            {
                _legacyLocomotion = false;
            }

        }

        get => _legacyLocomotion;
    }

    #endregion

    public bool setOnStart = true;

    #region cache movement
    private float jump_c;
    private float walk_c;
    private float strafe_c;
    private float run_c;
    private float gravity_c;
    #endregion

    private VRCPlayerApi user;

    void Start()
    {
        user = Networking.LocalPlayer;

        if (setOnStart)
        {
            SetMovement();
        }
    }

    public void SetMovement()
    {
        JumpValue = _jumpImpulse;
        WalkValue = _walkSpeed;
        StrafeValue = _strafeSpeed;
        RunValue = _runSpeed;
        GravityValue = _gravityStrength;
        LegacyLocomotion = _legacyLocomotion;
    }

    public override void OnPlayerTriggerEnter(VRC.SDKBase.VRCPlayerApi player)
    {
        if (!player.isLocal)
        {
            return;
        }

        jump_c = user.GetJumpImpulse();
        walk_c = user.GetWalkSpeed();
        strafe_c = user.GetStrafeSpeed();
        run_c = user.GetRunSpeed();
        gravity_c = user.GetGravityStrength();

        SetMovement();
    }
    public override void OnPlayerTriggerExit(VRC.SDKBase.VRCPlayerApi player)
    {
        if (!player.isLocal)
        {
            return;
        }

        user.SetJumpImpulse(jump_c);
        user.SetWalkSpeed(walk_c);
        user.SetStrafeSpeed(strafe_c);
        user.SetRunSpeed(run_c);
        user.SetGravityStrength(gravity_c);
    }
}
}

