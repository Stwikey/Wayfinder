using UnityEngine;

public class BoatMovement : MonoBehaviour {

    public float acceleration = 5.0f;
    public float deacceleration = 5.0f;
    public float maxSpeed = 5.0f;

    public float rotationAcceleration = 90.0f;
    public float rotationDeacceleration = 180.0f;
    public float maxRotationSpeed = 90.0f;

    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb = GetComponent<Rigidbody>();

        // converting rotation to rads
        rotationAcceleration = rotationAcceleration / 180 * Mathf.PI;
        rotationDeacceleration = rotationDeacceleration / 180 * Mathf.PI;
        maxRotationSpeed = maxRotationSpeed / 180 * Mathf.PI;
    }

    
    // Update is called once per frame
    void Update() {

    }

    float prevRotationSpeed = 0.0f;
    float prevMovementSpeed = 0.0f;

    void FixedUpdate() {
        if (GameManager.gameState != GameState.Movement && GameManager.gameState != GameState.Fishing) {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            prevRotationSpeed = 0.0f;
            prevMovementSpeed = 0.0f;
        }
        if (Camera.main.GetComponent<CameraMovement>().cameraState == CameraState.Panning) {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            prevRotationSpeed = 0.0f;
            prevMovementSpeed = 0.0f;
        }

        float deltaTime = Time.deltaTime;

        bool moveForward = (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W));
        bool moveBackward = (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S));
        bool turnRight = (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D));
        bool turnLeft = (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A));

        float curMaxSpeed = maxSpeed;
        float curAcceleration = acceleration;
        if (Input.GetKey(KeyCode.LeftShift)) {
            curMaxSpeed = maxSpeed * 2;
            curAcceleration = acceleration * 2;
        }

        float movementSpeed = rb.linearVelocity.magnitude;
        float rotationSpeed = rb.angularVelocity.magnitude;

        if (prevMovementSpeed < 0.0f) movementSpeed = -movementSpeed;
        if (prevRotationSpeed < 0.0f) rotationSpeed = -rotationSpeed;

        // moving forwards / backwards
        if (moveForward && movementSpeed >= 0.0f) {
            movementSpeed = Mathf.Min(curMaxSpeed, movementSpeed + curAcceleration * deltaTime);
        }
        else if (moveBackward && movementSpeed <= 0.0f) {
            movementSpeed = Mathf.Max(-curMaxSpeed, movementSpeed - curAcceleration * deltaTime);
        }
        // deaccelerating
        else {
            if (movementSpeed > 0) {
                movementSpeed = Mathf.Max(movementSpeed - acceleration * deltaTime, 0.0f);
            }
            else if (movementSpeed < 0) {
                movementSpeed = Mathf.Min(movementSpeed + acceleration * deltaTime, 0.0f);
            }
        }

        // turning
        if ((turnRight && rotationSpeed >= 0.0f && !moveBackward) || (turnLeft && rotationSpeed >= 0.0f && moveBackward)) {
            rotationSpeed = Mathf.Min(maxRotationSpeed, rotationSpeed + rotationAcceleration * deltaTime);
        }
        else if ((turnLeft && rotationSpeed <= 0.0f && !moveBackward) || (turnRight && rotationSpeed <= 0.0f && moveBackward)) {
            rotationSpeed = Mathf.Max(-maxRotationSpeed, rotationSpeed - rotationAcceleration * deltaTime);
        }
        else {
            if (rotationSpeed > 0) {
                rotationSpeed = Mathf.Max(rotationSpeed - rotationDeacceleration * deltaTime, 0.0f);
            }
            else if (rotationSpeed < 0) {
                rotationSpeed = Mathf.Min(rotationSpeed + rotationDeacceleration * deltaTime, 0.0f);
            }
        }

        rb.linearVelocity = transform.forward * movementSpeed;
        rb.angularVelocity = new Vector3(0, rotationSpeed, 0);

        prevMovementSpeed = movementSpeed;
        prevRotationSpeed = rotationSpeed;

    }
}
