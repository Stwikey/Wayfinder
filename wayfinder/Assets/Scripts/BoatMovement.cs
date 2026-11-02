using UnityEngine;

public class BoatMovement : MonoBehaviour {

    public Rigidbody rb;

    public float acceleration = 5.0f;
    public float deacceleration = 5.0f;
    public float maxSpeed = 5.0f;

    public float rotationAcceleration = 90.0f;
    public float rotationDeacceleration = 180.0f;
    public float maxRotationSpeed = 90.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    float rotationSpeed = 0.0f;
    float movementSpeed = 0.0f;
    // Update is called once per frame
    void Update() {

        if (GameManager.gameState != GameState.Movement) return;

        float deltaTime = Time.deltaTime;

        bool moveForward = (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W));
        bool moveBackward = (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S));
        bool turnRight = (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D));
        bool turnLeft = (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A));

        // moving forwards / backwards
        if (moveForward && movementSpeed >= 0.0f) {
            movementSpeed = Mathf.Min(maxSpeed, movementSpeed + acceleration * deltaTime);
        }
        else if (moveBackward && movementSpeed <= 0.0f) {
            movementSpeed = Mathf.Max(-maxSpeed, movementSpeed - acceleration * deltaTime);
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
        if ((moveForward || moveBackward) && turnRight && rotationSpeed >= 0.0f) {
            rotationSpeed = Mathf.Min(maxRotationSpeed, rotationSpeed + rotationAcceleration * deltaTime);
        }
        else if ((moveForward || moveBackward) && turnLeft && rotationSpeed <= 0.0f) {
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

        transform.position = transform.position + transform.forward * movementSpeed * deltaTime;
        transform.rotation *= Quaternion.Euler(0, rotationSpeed * deltaTime, 0);

    }
}
