using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingSequence : MonoBehaviour {
    public List<GameObject> gameObjects;
    void Update() {
        if (StoryManager.I().currentStoryStep == StoryStep.Finished && Camera.main.GetComponent<CameraMovement>().cameraState == CameraState.Stationary) {

        }
    }
}