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

public class CarControls : UdonSharpBehaviour
{
    public WheelCollider[] wheels = new WheelCollider[4]; //turn angle is only applied to the first two in array.
    public float wheelTorque = 600f;
    public AnimationCurve turnCurve = AnimationCurve.EaseInOut(5f, 55f, 40f, 10f); //used a curve so you change change the turn angle based on speed. time = velocity, value = turn angle;
    public float breakTorque = 900000f;
    public float downwardForce = 100f;
    public float maxSpeed = 50f;
    [Range(0f, 1f)]
    public float backwardsMultiplier = .5f;
    public float flipCorrectionForce = 10;
    public bool fourWheelDrive = true; //true = apply torque to all wheels || false = doesnt apply force to front two wheels.
    public Transform centerofmass;
    public Transform respawn;
    public CarStation station;

    private bool inCar;
    private Transform transform;
    private Rigidbody carrigid;
    private float forward;
    private float turn;
    private bool breakw;
    private VRC_Pickup pickup;

    private VRCPlayerApi user;

    void Start()
    {
        user = Networking.LocalPlayer;
        carrigid = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        pickup = (VRC_Pickup)GetComponent(typeof(VRC_Pickup));
        pickup.pickupable = false;
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].brakeTorque = float.MaxValue;
        }

        carrigid.centerOfMass = centerofmass.localPosition;
    }

    private void FixedUpdate()
    {
        
        if (!inCar)
        {
            return;
        }

        float motor = wheelTorque * forward;

        float steering = turnCurve.Evaluate(carrigid.velocity.magnitude) * turn;

        
        if(motor < 0f) //reduces torque going backwards
        {
            motor *= backwardsMultiplier;
        }
        

        //torque applied to wheels.
        for (int i = 0; i < wheels.Length; i++)
        {
            if (i > 1 || fourWheelDrive)
            {
                wheels[i].motorTorque = motor;
            }
        }

        //breaks applied to wheels
        foreach (WheelCollider wheel in wheels)
        {
            if (breakw)
            {
                wheel.brakeTorque = breakTorque;
            }
            else
            {
                wheel.brakeTorque = 0f;
                if (wheel.motorTorque == 0f)
                {
                    wheel.motorTorque = .01f; //wheel doesnt wake up unless you change the value when break is at 0
                }
            }
            
        }
        
        //downforce if grounded. flips up if in air.
        if (WheelsGrounded())
        {
            carrigid.AddForce(-transform.up * Mathf.Clamp(carrigid.velocity.magnitude, 0f, downwardForce), ForceMode.Acceleration);
        }
        else
        {
            Vector3 flipdirection = (Quaternion.FromToRotation(transform.up, Vector3.up) * Vector3.up);
            
            if(flipdirection.y >= 0f)
            {
                flipdirection *= Mathf.Abs(flipCorrectionForce);
                carrigid.AddTorque(flipdirection.z, 0f, flipdirection.x * -1f, ForceMode.Acceleration);
            }
            else
            {
                flipdirection = Vector3.Scale(flipdirection, new Vector3(1f,0f,1f));
                flipdirection = flipdirection.normalized;
                flipdirection *= Mathf.Abs(flipCorrectionForce);
                carrigid.AddTorque(flipdirection.z, 0f, flipdirection.x * -1f, ForceMode.Acceleration);
            }

        }

        //steer angle
        for (int i = 0; i < 2; i++)
        {
            wheels[i].steerAngle = steering;
        }

        //clamp speed
        carrigid.velocity = Vector3.ClampMagnitude(carrigid.velocity, Mathf.Abs(maxSpeed));
    }

    public virtual void InputMoveHorizontal(float value, VRC.Udon.Common.UdonInputEventArgs args) 
    {
        if (inCar && !user.IsUserInVR())
        {
            turn = value;
        }
    }
    public virtual void InputMoveVertical(float value, VRC.Udon.Common.UdonInputEventArgs args) 
    {
        if (inCar)
        {
            forward = value;
        }
    }
    public virtual void InputLookHorizontal(float value, VRC.Udon.Common.UdonInputEventArgs args) 
    {
        if (inCar && user.IsUserInVR())
        {
            turn = value;
        }
    }
    public virtual void InputJump(bool value, VRC.Udon.Common.UdonInputEventArgs args) 
    {
        if (inCar)
        {
            breakw = value;
        }
        
    }

    public bool WheelsGrounded()
    {

        foreach(WheelCollider wheel in wheels)
        {
            if (!wheel.isGrounded)
            {
                return false;
            }

        }

        return true;

    }

    public void EnteredCar()
    {
        Networking.SetOwner(user, gameObject);
        inCar = true;

        turn = 0f;
        forward = 0f;
        breakw = false;

        pickup.Drop();
        pickup.pickupable = false;
        
    }

    public void ExitedCar()
    {
        inCar = false;

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].brakeTorque = float.MaxValue;
        }

        carrigid.velocity = Vector3.zero;
        carrigid.angularVelocity = Vector3.zero;

        turn = 0f;
        forward = 0f;
        breakw = false;

        pickup.pickupable = true;
    }

    public void RespawnCar()
    {
        if (!Utilities.IsValid(station.seated))
        {
            Networking.SetOwner(user, gameObject);
            carrigid.rotation = respawn.rotation;
            carrigid.position = respawn.position;
        }
    }

    public void RemoteEnteredCar()
    {
        pickup.pickupable = false;
    }
  
}
