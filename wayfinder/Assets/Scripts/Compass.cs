using UnityEngine;

public class Compass : MonoBehaviour {
    public GameObject trackObject;
    public GameObject currentObject;

    void Start() {

    }

    void Update() {

        // getting vectors
        Vector3 forward = new Vector3(currentObject.transform.forward.x, 0, currentObject.transform.forward.z);
        Vector3 pos = new Vector3(trackObject.transform.position.x - currentObject.transform.position.x, 0, trackObject.transform.position.z - currentObject.transform.position.z);

        // normalizing
        pos = pos.normalized;
        forward = forward.normalized;

        float angle = Mathf.Acos(Vector3.Dot(forward, pos));
        float angleDeg = angle * 180 / Mathf.PI;

        // flipping angle if required
        if (Vector3.Cross(forward, pos).y > 0) angleDeg *= -1;

        transform.rotation = Quaternion.Euler(0, 0, angleDeg);
    }
}
