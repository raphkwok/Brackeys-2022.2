using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    private Vector2 playerInput;
    private float wheelAngle;

    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public float maxSteerAngle = 30;
    public float speed = 50;

    public void OnMovement(InputValue value)
    {
        playerInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        print(playerInput);

        Steer();
        Accelerate();
        UpdateWheelPoses();
    }

    private void Steer()
    {
        wheelAngle = maxSteerAngle * playerInput.x;
        frontDriverW.steerAngle = wheelAngle;
        frontPassengerW.steerAngle = wheelAngle;
    }

    private void Accelerate()
    {
        frontDriverW.motorTorque = playerInput.y * speed;
        frontPassengerW.motorTorque = playerInput.y * speed;
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
        wheel.rotation = quat;
    }
}
