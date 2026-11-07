using System;
using System.Collections.Generic;
using UnityEngine;

public enum CameraState {
    OnlyFollowing,
    Controllable,
    Panning,
    Stationary
};

[Serializable]
public struct CameraPanningMovement {
    public Vector3 position;
    public Vector3 rotation;
    public float time;
    public float waitTime;

    public CameraPanningMovement(Vector3 p, Vector3 r, float t, float wt) {
        position = p;
        rotation = r;
        time = t;
        waitTime = wt;
    }
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

    public CameraState cameraState = CameraState.Controllable;

    public List<CameraPanningMovement> cameraPanningMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        distance = (transform.position - followingObj.transform.position).magnitude;
        rotation = transform.rotation.eulerAngles;
    }

    void Update() {

        if (cameraState != CameraState.Controllable) return;
        if (GameManager.gameState != GameState.Movement) return;

        float mouseScroll = Input.mouseScrollDelta.y;
        Vector3 mouseDeltaRaw = Input.mousePositionDelta;
        Vector3 mouseDelta = new Vector3(-mouseDeltaRaw.y, mouseDeltaRaw.x, 0.0f);

        distance += -mouseScroll * zoomSpeed;
        distance = Mathf.Max(Mathf.Min(distance, maxDistance), minDistance);

        rotation = rotation + new Vector3(mouseDelta.x * rotateSpeed, mouseDelta.y * rotateSpeed, 0);
        rotation = new Vector3(Mathf.Max(Mathf.Min(rotation.x, maxRotationX), minRotationX), rotation.y, rotation.z);
    }

    // move camera to the correct place to track what it is following
    void LateUpdate() {

        if (cameraState == CameraState.Stationary) return;

        if (cameraState == CameraState.Panning) {
            CameraPan();
        }
        else if (cameraState == CameraState.Controllable || cameraState == CameraState.OnlyFollowing) {
            transform.rotation = followingObj.transform.rotation * Quaternion.Euler(rotation.x, rotation.y, rotation.z);
            transform.position = followingObj.transform.position - transform.forward * distance;
        }
    }

    int panningIdx;
    float panningElapsedTime;
    bool panning;

    Vector3 originalPosition;
    Vector3 newPosition;

    Vector3 originalRotation;
    Vector3 newRotation;

    void CameraPan() {
        float deltaTime = Time.deltaTime;
        panningElapsedTime += deltaTime;

        if (panning) {
            panningElapsedTime = Mathf.Min(panningElapsedTime, cameraPanningMovement[panningIdx].time);
            float fraction = panningElapsedTime / cameraPanningMovement[panningIdx].time;
            transform.position = Vector3.Lerp(originalPosition, newPosition, fraction);
            transform.rotation = Quaternion.Euler(Vector3.Lerp(originalRotation, newRotation, fraction));

            // start waiting
            if (panningElapsedTime == cameraPanningMovement[panningIdx].time) {
                panningElapsedTime = 0;
                panning = false;
            }
        }
        else {
            if (panningElapsedTime > cameraPanningMovement[panningIdx].waitTime) {
                if (panningIdx == cameraPanningMovement.Count - 1) {
                    cameraState = CameraState.Controllable;
                    cameraPanningMovement = new List<CameraPanningMovement>();
                }
                else {
                    panningIdx++;
                    SetupPanning(panningIdx);
                }
            }
        }
    }

    public void StartPanning() {
        cameraState = CameraState.Panning;
        SetupPanning(0);
    }

    void SetupPanning(int idx) {
        panningIdx = 0;
        panningElapsedTime = 0;
        panning = true;

        originalPosition = transform.position;
        originalRotation = transform.rotation.eulerAngles;
        newPosition = cameraPanningMovement[idx].position;
        newRotation = cameraPanningMovement[idx].rotation;

        while (originalRotation.x > newRotation.x) {
            if (Mathf.Abs(originalRotation.x - 360 - newRotation.x) < Mathf.Abs(originalRotation.x - newRotation.x)) {
                originalRotation = new Vector3(originalRotation.x - 360, originalRotation.y, originalRotation.z);
            }
            else break;
        }

        while (newRotation.x > originalRotation.x) {
            if (Mathf.Abs(newRotation.x - 360 - originalRotation.x) < Mathf.Abs(newRotation.x - originalRotation.x)) {
                newRotation = new Vector3(newRotation.x - 360, newRotation.y, newRotation.z);
            }
            else break;
        }

        while (originalRotation.y > newRotation.y) {
            if (Mathf.Abs(originalRotation.y - 360 - newRotation.y) < Mathf.Abs(originalRotation.y - newRotation.y)) {
                originalRotation = new Vector3(originalRotation.x, originalRotation.y - 360, originalRotation.z);
            }
            else break;
        }

        while (newRotation.y > originalRotation.y) {
            if (Mathf.Abs(newRotation.y - 360 - originalRotation.y) < Mathf.Abs(newRotation.y - originalRotation.y)) {
                newRotation = new Vector3(newRotation.x, newRotation.y - 360, newRotation.z);
            }
            else break;
        }

        while (originalRotation.z > newRotation.z) {
            if (Mathf.Abs(originalRotation.z - 360 - newRotation.z) < Mathf.Abs(originalRotation.z - newRotation.z)) {
                originalRotation = new Vector3(originalRotation.x, originalRotation.y, originalRotation.z - 360);
            }
            else break;
        }

        while (newRotation.z > originalRotation.z) {
            if (Mathf.Abs(newRotation.z - 360 - originalRotation.z) < Mathf.Abs(newRotation.z - originalRotation.z)) {
                newRotation = new Vector3(newRotation.x, newRotation.y, newRotation.z - 360);
            }
            else break;
        }
        
    }
}
