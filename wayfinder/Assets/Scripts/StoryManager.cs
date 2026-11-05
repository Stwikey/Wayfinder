using UnityEngine;

public enum StoryStep {
    WSMovement,
    ADMovement,
    Fishing,
    FishingWASD,
    DoorOpen,
};

public class StoryManager : MonoBehaviour {

    public GameObject WSMovementUI;
    public GameObject ADMovementUI;
    public GameObject FishingUI;
    public GameObject FishingWASDUI;
    

    public StoryStep currentStoryStep;

    void Start() {
        UpdateStoryStep(currentStoryStep);
    }

    void Update() {

    }

    void UpdateStory() {
        
    }

    void UpdateStoryStep(StoryStep storyStep) {

        if (storyStep == StoryStep.WSMovement) WSMovementUI.SetActive(true);
        else WSMovementUI.SetActive(false);

        if (storyStep == StoryStep.ADMovement) ADMovementUI.SetActive(true);
        else ADMovementUI.SetActive(false);

        if (storyStep == StoryStep.Fishing) FishingUI.SetActive(true);
        else FishingUI.SetActive(false);

        if (storyStep == StoryStep.FishingWASD) FishingWASDUI.SetActive(true);
        else FishingWASDUI.SetActive(false);

        currentStoryStep = storyStep;

    }
}
