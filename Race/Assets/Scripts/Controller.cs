using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public KeyCode Right;
    public KeyCode Left;


    private const string HORIZONTAL = "Horizontal2";
    private const string VERTICAL = "Vertical2";

    private float horizontalInput;
    private float verticalInput;
    private float Angle;
    private float currentBreakForce;
    private bool breaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteeringAngle;

    [SerializeField] private WheelCollider frontLeft;
    [SerializeField] private WheelCollider frontRight;
    [SerializeField] private WheelCollider RearLeft;
    [SerializeField] private WheelCollider RearRight;

    [SerializeField] private Transform frontLeftT;
    [SerializeField] private Transform frontRightT;
    [SerializeField] private Transform RearLeftT;
    [SerializeField] private Transform RearRightT;

    private void Update()
    {
        if (Input.GetKeyDown(Right))
            GetComponent<Transform>().eulerAngles = new Vector3(0, 90, 0);
        if (Input.GetKeyDown(Left))
            GetComponent<Transform>().eulerAngles = new Vector3(0, 90, 0);
    }

    private void FixedUpdate()
    {
        GetInput();
        Motor();
        Steer();
        WheelVisuals();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        breaking = Input.GetKey(KeyCode.Space);
    }

    private void Motor()
    {
        frontLeft.motorTorque = verticalInput * motorForce * 2;
        frontRight.motorTorque = verticalInput * motorForce * 2;
        currentBreakForce = breaking ? breakForce : 0f;
        if (breaking)
        {
            BeginBreaking();
        }
    }

    private void Steer()
    {
        Angle = maxSteeringAngle * horizontalInput;
        frontLeft.steerAngle = Angle;
        frontRight.steerAngle = Angle;
    }

    private void WheelVisuals()
    {
        UpdateSingleWheel(frontLeft, frontLeftT);
        UpdateSingleWheel(frontRight, frontRightT);
        UpdateSingleWheel(RearLeft, RearLeftT);
        UpdateSingleWheel(RearRight, RearRightT);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform WTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        WTransform.rotation = rot;
        WTransform.position = pos;
    }

    private void BeginBreaking()
    {
        frontLeft.brakeTorque = currentBreakForce;
        frontRight.brakeTorque = currentBreakForce;
        RearLeft.brakeTorque = currentBreakForce;
        RearRight.brakeTorque = currentBreakForce;
    }

   
}
