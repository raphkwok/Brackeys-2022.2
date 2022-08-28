using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    private Vector2 playerInput;
    private float wheelAngle;
    private bool braking;

    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public float maxSteerAngle = 30;
    public float steerSpeed = 70;
    public float speed = 1200;
    public float brakeStrength = 1800;


    //Extras
    [SerializeField] private GameObject steeringWheel;

    public void OnMovement(InputValue value)
    {
        playerInput = value.Get<Vector2>();
    }

    public void OnBrake(InputValue value)
    {
        braking = true;
        Brake(frontDriverW);
        Brake(frontPassengerW);
    }

    public void OnAccelerate(InputValue value)
    {
        braking = false;
        Brake(frontDriverW);
        Brake(frontPassengerW);
    }

    private void Brake(WheelCollider collider)
    {
        if (braking)
        {
            collider.brakeTorque = brakeStrength;
        }
        else
        {
            collider.brakeTorque = 0f;
        }
    }

    private void FixedUpdate()
    {

        Steer();
        Accelerate();
        UpdateWheelPoses();
    }

    private void Steer()
    {
        wheelAngle = Mathf.MoveTowards(wheelAngle, maxSteerAngle * playerInput.x, Time.deltaTime * steerSpeed);
        steeringWheel.transform.localEulerAngles = Vector3.forward * -wheelAngle * 1.5f;
        print(steeringWheel.transform.rotation);


        frontDriverW.steerAngle = wheelAngle;
        frontPassengerW.steerAngle = wheelAngle;
    }

    private void Accelerate()
    {
        /**
        frontDriverW.motorTorque = playerInput.y * speed;
        frontPassengerW.motorTorque = playerInput.y * speed;
        **/
        frontDriverW.motorTorque = speed;
        frontPassengerW.motorTorque = speed;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(frontPassengerW, frontPassengerT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(rearPassengerW, rearPassengerT);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform wheel)
    {
        Vector3 pos = wheel.position;
        Quaternion quat = wheel.rotation;

        collider.GetWorldPose(out pos, out quat);

        wheel.position = pos;
        wheel.rotation = quat * Quaternion.Euler(Vector3.right * 90f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            GameManager.gm.Loop();
        }
        else if (other.gameObject.tag == "Death")
        {
            GameManager.gm.Death();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Finish")
        {
            GameManager.gm.Loop();
        }
        else if (other.gameObject.tag == "Death")
        {
            GameManager.gm.Death();
        }
    }
}
