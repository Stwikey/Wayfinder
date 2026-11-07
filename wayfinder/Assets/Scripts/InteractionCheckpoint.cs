using UnityEngine;

public class InteractionCheckpoint : MonoBehaviour {
    public bool interacted = false;

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Player") interacted = true;
    }
};