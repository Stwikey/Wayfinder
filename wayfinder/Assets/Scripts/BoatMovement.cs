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
    // Update is called once per frame
    void Update() {

        float deltaTime = Time.deltaTime;

        // accelerate
        float speed = Mathf.Max(rb.linearVelocity.magnitude);
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            // turning
            bool turnRight = (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D));
            bool turnLeft = (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A));
            if (turnRight && rotationSpeed >= 0.0f) {
                rotationSpeed = Mathf.Min(maxRotationSpeed, rotationSpeed + rotationAcceleration * deltaTime);
            }
            else if (turnLeft && rotationSpeed <= 0.0f) {
                rotationSpeed = Mathf.Max(-maxRotationSpeed, rotationSpeed - rotationAcceleration * deltaTime);
            }
            else if (!turnLeft && !turnRight) {
                if (rotationSpeed > 0) {
                    rotationSpeed = Mathf.Max(rotationSpeed - rotationDeacceleration * deltaTime, 0.0f);
                }
                else if (rotationSpeed < 0) {
                    rotationSpeed = Mathf.Min(rotationSpeed + rotationDeacceleration * deltaTime, 0.0f);
                }
            }
            speed += acceleration * deltaTime;
        }
        // otherwise deaccelerate
        else {
            speed = Mathf.Max(speed - deacceleration * deltaTime, 0.0f);
            if (rotationSpeed > 0) {
                rotationSpeed = Mathf.Max(rotationSpeed - rotationDeacceleration * deltaTime, 0.0f);
            }
            else if (rotationSpeed < 0) {
                rotationSpeed = Mathf.Min(rotationSpeed + rotationDeacceleration * deltaTime, 0.0f);
            }
        }

        speed = Mathf.Min(speed, maxSpeed);
        
        rb.linearVelocity = transform.forward * speed;
        transform.rotation *= Quaternion.Euler(0, rotationSpeed * deltaTime, 0);

    }
}
