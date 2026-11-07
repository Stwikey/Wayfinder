using UnityEngine;

public enum StoryStep {
    StartGame,
    StartGameAnimation,
    StartGamePanCamera,
    WASDMovement,
    ZoomCamera,
    ReachFishing,
    FishingEnter,
    Fishing,
    FishingWASD,
    DoorOpen,
};

public class StoryManager : MonoBehaviour {

    // Singleton

    private static StoryManager _instance;

    private StoryManager() {
        _instance = this;
    }

    public static StoryManager I() {
        if (_instance == null) {
            StoryManager instance = new StoryManager();
            _instance = instance;
        }
        return _instance;
    }

    public GameObject StartTitleUI;
    public GameObject ClickAnywhereToBeginUI;

    public GameObject WASDMovementUI;
    public GameObject FishingUI;
    public GameObject FishingWASDUI; 

    public StoryStep currentStoryStep;

    void Start() {
        UpdateStoryStep(currentStoryStep);
    }

    void Update() {
        UpdateStory();
    }

    void UpdateStory() {
        if (currentStoryStep == StoryStep.StartGame) {
            if (Input.GetMouseButtonDown(0)) {
                UpdateStoryStep(StoryStep.StartGameAnimation);
                StartTitleUI.GetComponent<StartGameAnimation>().StartAnimation();
            }
        }

        if (currentStoryStep == StoryStep.StartGamePanCamera) {
            if (Camera.main.GetComponent<CameraMovement>().cameraState != CameraState.Panning) {
                UpdateStoryStep(StoryStep.ZoomCamera);
            }
        }

        if (currentStoryStep == StoryStep.ZoomCamera) {
            if (Mathf.Abs(Input.mouseScrollDelta.y) > 0.01f) {
                UpdateStoryStep(StoryStep.WASDMovement);
            }
        }

        if (currentStoryStep == StoryStep.WASDMovement) {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow)) UpdateStoryStep(StoryStep.ReachFishing);
        }

        if (currentStoryStep == StoryStep.FishingEnter) {
            if (GameManager.gameState == GameState.Fishing) UpdateStoryStep(StoryStep.Fishing);
        }

        if (currentStoryStep == StoryStep.Fishing) {
            if (GameManager.gameState != GameState.Fishing) UpdateStoryStep(StoryStep.FishingEnter);
        }
    }

    // used on changing the storystep
    public void UpdateStoryStep(StoryStep storyStep) {

        if (storyStep == StoryStep.StartGame) {
            ClickAnywhereToBeginUI.SetActive(true);
            Camera.main.transform.position = new Vector3(-10.36454f, 74.9f, 78.3f);
            Camera.main.transform.rotation = Quaternion.Euler(-5.772f, -3.754f, -0.376f);

            Camera.main.GetComponent<CameraMovement>().cameraState = CameraState.Stationary;
        }
        else ClickAnywhereToBeginUI.SetActive(false);

        if (storyStep == StoryStep.StartGame || storyStep == StoryStep.StartGameAnimation) StartTitleUI.SetActive(true);
        else StartTitleUI.SetActive(false);

        if (storyStep == StoryStep.StartGamePanCamera) {
            Camera.main.GetComponent<CameraMovement>().cameraPanningMovement.Add(new CameraPanningMovement(new Vector3(-10.3645372f, 34.6417847f, -32.184639f), new Vector3(50.0000114f, 14.74617f, 359.623962f), 4, 0));
            Camera.main.GetComponent<CameraMovement>().StartPanning();
        }

        if (storyStep == StoryStep.WASDMovement) WASDMovementUI.SetActive(true);
        else WASDMovementUI.SetActive(false);

        if (storyStep == StoryStep.Fishing) FishingUI.SetActive(true);
        else FishingUI.SetActive(false);

        if (storyStep == StoryStep.FishingWASD) FishingWASDUI.SetActive(true);
        else FishingWASDUI.SetActive(false);

        currentStoryStep = storyStep;

    }
}
