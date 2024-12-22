using UnityEngine;

public class CartController : MonoBehaviour
{
    public WheelColliders colliders;
    public WheelMeshes wheelMeshes;
    private Rigidbody cartRB;
    public GameObject tireTrail;

    public float gasInput;
    public float steeringInput;
    public float brakeInput;

    public float motorPower;
    public float brakePower;
    private float speed;
    public AnimationCurve steeringCurve;
    private float slipAngle;

    public PlayerButton gasPedal;
    public PlayerButton brakePedal;
    public PlayerButton leftButton;
    public PlayerButton rightButton;

    private void Start()
    {
        cartRB = GetComponent<Rigidbody>();
        colliders.wheelFL.ConfigureVehicleSubsteps(5, 12, 15);
        colliders.wheelFR.ConfigureVehicleSubsteps(5, 12, 15);
        colliders.wheelRL.ConfigureVehicleSubsteps(5, 12, 15);
        colliders.wheelRR.ConfigureVehicleSubsteps(5, 12, 15);
    }
    private void Update()
    {
        speed=cartRB.velocity.magnitude;
        CheckInput();
        UpdateWheels();
    }
    private void FixedUpdate()
    {
        ApplyMotor();
        ApplyBreak();
        ApplySteering();
    }

    void CheckInput()
    {
        gasInput = Input.GetAxis("Vertical");
        if (gasPedal.isPressed) {
            gasInput += gasPedal.dampenPress;
        }
        if (brakePedal.isPressed)
        {
            gasInput -= gasPedal.dampenPress;
        }
        steeringInput = Input.GetAxis("Horizontal");
        if (rightButton.isPressed)
        {
            steeringInput += rightButton.dampenPress;
        }
        if (leftButton.isPressed)
        {
            steeringInput -= leftButton.dampenPress;
        }
        slipAngle = Vector3.Angle(transform.forward, cartRB.velocity - transform.forward);

        float movingDirection = Vector3.Dot(transform.forward, cartRB.velocity);
        if (movingDirection < -0.5f && gasInput > 0)
        {
            brakeInput = Mathf.Abs(gasInput);
        }
        else if (movingDirection > 0.5f && gasInput < 0)
        {
            brakeInput = Mathf.Abs(gasInput);
        }
        else
        {
            brakeInput = 0;
        }

    }
    void ApplyBreak()
    {
        colliders.wheelFR.brakeTorque = brakeInput * brakePower * 0.3f;
        colliders.wheelFL.brakeTorque = brakeInput * brakePower * 0.3f;
        colliders.wheelRR.brakeTorque = brakeInput * brakePower * 0.7f;
        colliders.wheelRL.brakeTorque = brakeInput * brakePower * 0.7f;
    }
    void ApplyMotor()
    {
        colliders.wheelFR.motorTorque = motorPower * gasInput * 0.3f;
        colliders.wheelFL.motorTorque = motorPower * gasInput * 0.3f;
        colliders.wheelRR.motorTorque = motorPower * gasInput * 0.7f;
        colliders.wheelRL.motorTorque = motorPower * gasInput * 0.7f;
    }


    void ApplySteering()
    {
        float steeringAngle = steeringInput * steeringCurve.Evaluate(speed);
        if (slipAngle < 90f)
        {
            steeringAngle += Vector3.SignedAngle(transform.forward, cartRB.velocity + transform.forward, Vector3.up);
        }
        steeringAngle = Mathf.Clamp(steeringAngle, -90f, 90f);
        colliders.wheelFR.steerAngle = steeringAngle;
        colliders.wheelFL.steerAngle = steeringAngle;
    }
    
   void UpdateWheels()
    {
        UpdateWheel(colliders.wheelFR, wheelMeshes.wheelFR);
        UpdateWheel(colliders.wheelFL, wheelMeshes.wheelFL);
        UpdateWheel(colliders.wheelRR, wheelMeshes.wheelRR);
        UpdateWheel(colliders.wheelRL, wheelMeshes.wheelRL);
    }
    void UpdateWheel(WheelCollider coll, MeshRenderer wheelMesh)
    {
        Quaternion quat;
        Vector3 position;
        coll.GetWorldPose(out position, out quat);
        wheelMesh.transform.position = position;
        wheelMesh.transform.rotation = quat;
    }

}

[System.Serializable]
public class WheelColliders
{
    public WheelCollider wheelFR;
    public WheelCollider wheelFL;
    public WheelCollider wheelRR;
    public WheelCollider wheelRL;
}
[System.Serializable]
public class WheelMeshes
{
    public MeshRenderer wheelFR;
    public MeshRenderer wheelFL;
    public MeshRenderer wheelRR;
    public MeshRenderer wheelRL;
}