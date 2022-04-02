using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RacerController : MonoBehaviour
{
    [Header("Default Values")]
    [SerializeField] float maxSpeed;
    [SerializeField] float turningSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float brakingDeceleration;
    [SerializeField] float groundDeceleration;
    [SerializeField] float reverseMaxSpeed;
    [SerializeField] float airDeceleration;
    [SerializeField] float gravity;
    [SerializeField] float jumpSpeed;
    [SerializeField] float maxFallSpeed;

    [Header("Debug UI")]
    [SerializeField] GameObject debugCanvas;
    [SerializeField] bool showDebugCanvas;
    [SerializeField] TextMeshProUGUI currentSpeedText;
    [SerializeField] TextMeshProUGUI ySpeedText;

    Rigidbody rb;
    bool isgrounded;
    Vector3 newVelocity;
    Vector3 newRelativeVelocity;
    Quaternion newRotation;

    Vector2 directionInput;
    bool braking;
    bool jump1Requested;
    bool jump2Requested;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        newRelativeVelocity.z = transform.InverseTransformDirection(rb.velocity).z + directionInput.y * acceleration * Time.deltaTime;
        Debug.Log(directionInput);
        newRelativeVelocity.z = Mathf.Clamp(newRelativeVelocity.z, -Mathf.Abs(reverseMaxSpeed), maxSpeed);
    }

    void FixedUpdate()
    {
        rb.velocity = transform.TransformDirection(newRelativeVelocity);
    }

    public void GetInputs(RacerInputs inputs)
    {
        directionInput = inputs.directionInput;
        braking = inputs.braking;

        //if (Motor.GroundingStatus.IsStableOnGround)
        //{
        //    if (inputs.jump1Triggered) jump1Requested = true;
        //    if (inputs.jump2Triggered) jump2Requested = true;
        //}
    }

}
