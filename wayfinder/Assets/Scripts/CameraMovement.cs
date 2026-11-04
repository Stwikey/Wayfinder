using UnityEngine;

public enum CameraState {
    Following
};

public class CameraMovement : MonoBehaviour {

    public GameObject followingObj;
    
    public float maxDistance;
    public float minDistance;
    public float distance;
    public float zoomSpeed;

    public float minRotationX;
    public float maxRotationX;
    public float rotateSpeed;
    public Vector3 rotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        distance = (transform.position - followingObj.transform.position).magnitude;
        rotation = transform.rotation.eulerAngles;
    }

    void Update() {
        float mouseScroll = Input.mouseScrollDelta.y;
        Vector3 mouseDeltaRaw = Input.mousePositionDelta;
        Vector3 mouseDelta = new Vector3(-mouseDeltaRaw.y, mouseDeltaRaw.x, 0.0f);

        // distance += -mouseScroll * zoomSpeed;
        // distance = Mathf.Max(Mathf.Min(distance, maxDistance), minDistance);

        // rotation = rotation + new Vector3(mouseDelta.x * rotateSpeed, mouseDelta.y * rotateSpeed, 0);
        // rotation = new Vector3(Mathf.Max(Mathf.Min(rotation.x, maxRotationX), minRotationX), rotation.y, rotation.z);
    }

    // move camera to the correct place to track what it is following
    void LateUpdate() {
        transform.rotation = followingObj.transform.rotation * Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        transform.position = followingObj.transform.position - transform.forward * distance;
    }
}
