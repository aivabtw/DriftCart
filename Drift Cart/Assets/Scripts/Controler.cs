using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{
    public Rigidbody rb;

    // Wheel Transforms
    public Transform frontLeftWheelBase, frontRightWheelBase;
    public Transform backLeftWheelBase, backRightWheelBase;
    public Transform frontLeftWheel, frontRightWheel, backLeftWheel, backRightWheel;

    // Turning Points
    public Transform leftPoint;
    public Transform rightPoint;

    // Forces
    public float turnForce = 30f;
    public float wheelSpinSpeed = 10f;
    public float maxTurnAngle = 30f;

    //private Vector3 appliedForce;

    public PlayerButton leftButtonUp;
    public PlayerButton rightButtonUp;
    public PlayerButton leftButtonDown;
    public PlayerButton rightButtonDown;

    public bool InputLeftUp = false;
    public bool InputRightUp = false;
    public bool InputLeftDown = false;
    public bool InputRightDown = false;

    public Transform CenterOfMass;

    public bool canGo = false;

    private void Start()
    {
        rb.centerOfMass = CenterOfMass.localPosition;
    }

    void Update()
    {
        SpinWheels();
        GetInput();
    }
    private void FixedUpdate()
    {
        HandleInput();
    }


    private void GetInput()
    {
        if (Input.GetKey(KeyCode.Q) || leftButtonUp.isPressed)
        {
            InputLeftUp = true;
        } else {
            InputLeftUp = false;
        }
        if (Input.GetKey(KeyCode.E) || rightButtonUp.isPressed)
        {
            InputRightUp = true;
        } else {
            InputRightUp = false;
        }
        if (Input.GetKey(KeyCode.D) || rightButtonDown.isPressed)
        {
            InputRightDown = true;
        }
        else
        {
            InputRightDown = false;
        }
        if (Input.GetKey(KeyCode.A) || leftButtonDown.isPressed)
        {
            InputLeftDown = true;
        }
        else
        {
            InputLeftDown = false;
        }
    }


    private void HandleInput()
    {
        if (!canGo) return;
        // apply force at the left handle
        if (InputLeftUp)
        {
            Vector3 leftForceDirection = (transform.forward - Vector3.up * 0.1f).normalized;
            rb.AddForceAtPosition(leftForceDirection * turnForce, leftPoint.position, ForceMode.Force);
        }
        else if (InputLeftDown)
        {
            Vector3 leftForceDirection = 0.5f*(-transform.forward + Vector3.up * 0.1f).normalized;
            rb.AddForceAtPosition(leftForceDirection * turnForce, leftPoint.position, ForceMode.Force);
        }
        // apply force at the right handle  + Vector3.up * 0.1f
        if (InputRightUp)
        {
            Vector3 rightForceDirection = (transform.forward - Vector3.up * 0.1f).normalized;
            rb.AddForceAtPosition(rightForceDirection * turnForce, rightPoint.position, ForceMode.Force);
        }
        else if (InputRightDown)
        {
            Vector3 rightForceDirection = 0.5f*(-transform.forward + Vector3.up * 0.1f).normalized;
            rb.AddForceAtPosition(rightForceDirection * turnForce, rightPoint.position, ForceMode.Force);
        }
    }

    private void SpinWheels()
    {
        float spinAngle = rb.velocity.magnitude * wheelSpinSpeed * Time.deltaTime;

        frontLeftWheel.Rotate(Vector3.right, spinAngle);
        frontRightWheel.Rotate(Vector3.right, spinAngle);
        backLeftWheel.Rotate(Vector3.right, spinAngle);
        backRightWheel.Rotate(Vector3.right, spinAngle);
    }

}
