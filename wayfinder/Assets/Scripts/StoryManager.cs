using UnityEngine;

public enum StoryStep {
    StartGame,
    StartGameAnimation,
    StartGamePanCamera,
    ZoomCamera,
    WASDMovement,
    ReachFishing,
    FishingEnter,
    Fishing,
    Catch1,
    AfterConstellation1,
    FishingWASDEnter,
    FishingWASDFish,
    ReachFirstRune,
    Puzzle1,
    Catch2,
    AfterConstellation2,
    ReachSecondRune,
    Puzzle5,
    Catch3,
    AfterConstellation3,
    ReachThirdRune,
    Puzzle9,
    Catch4,
    AfterConstellation4,
    ReachDoor,
    ReachCompass,
    Finished
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

    public int puzzleCount = 0;

    public GameObject TitleCam;
    public GameObject Constellation1Cam;
    public GameObject TinyStars1Cam;
    public GameObject Constellation2Cam;
    public GameObject TinyStars2Cam;
    public GameObject Constellation3Cam;
    public GameObject TinyStars3Cam;
    public GameObject Constellation4Cam;
    public GameObject Rune1Cam;
    public GameObject EndingCam;

    public GameObject StartTitleUI;
    public GameObject ClickAnywhereToBeginUI;

    public GameObject ZoomUI;

    public GameObject WASDMovementUI;

    public GameObject ReachFishingObj;

    public GameObject FishingEnterUI;
    public GameObject FishingUI;

    public GameObject Catch1;
    public GameObject Constellation1;
    public GameObject Barrier1;
    public GameObject Tinystars1;

    public GameObject FishingWASDEnterUI;
    public GameObject FishingWASDFishUI;

    public GameObject ReachFirstRune;
    public GameObject Catch2;
    public GameObject Constellation2;
    public GameObject Barrier2;
    public GameObject Tinystars2;

    public GameObject ReachSecondRune;
    public GameObject Catch3;
    public GameObject Constellation3;
    public GameObject Barrier3;
    public GameObject Tinystars3;
    
    public GameObject ReachThirdRune;
    public GameObject Catch4;
    public GameObject Constellation4;

    public GameObject ReachDoor;

    public Animator doorAnimator;
    public GameObject Islands;
    
    public GameObject ReachCompass;

    public GameObject Compass;

    public StoryStep currentStoryStep;

    CameraMovement cam;


    void Start() {
        cam = Camera.main.GetComponent<CameraMovement>();
        UpdateStoryStep(currentStoryStep);
    }

    void Update() {
        UpdateStory();
    }

    void UpdateStory() {
        if (currentStoryStep == StoryStep.StartGame) {
            UpdateStoryStep(StoryStep.StartGameAnimation);
            StartTitleUI.GetComponent<StartGameAnimation>().StartAnimation();
            
        }

        if (currentStoryStep == StoryStep.StartGamePanCamera) {
            if (cam.cameraState != CameraState.Panning) {
                UpdateStoryStep(StoryStep.WASDMovement);
            }
        }

        // if (currentStoryStep == StoryStep.ZoomCamera) {
        //     if (Mathf.Abs(Input.mouseScrollDelta.y) > 0.1f) {
        //         UpdateStoryStep(StoryStep.WASDMovement);
        //     }
        // }
        

        if (currentStoryStep == StoryStep.WASDMovement) {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow)) UpdateStoryStep(StoryStep.ReachFishing);
        }

        if (currentStoryStep == StoryStep.ReachFishing) {
            if (ReachFishingObj.GetComponent<InteractionCheckpoint>().interacted) UpdateStoryStep(StoryStep.FishingEnter);
        }

        if (currentStoryStep == StoryStep.FishingEnter) {
            if (GameManager.gameState == GameState.Fishing) UpdateStoryStep(StoryStep.Fishing);
        }

        if (currentStoryStep == StoryStep.Fishing) {
            if (GameManager.gameState != GameState.Fishing) UpdateStoryStep(StoryStep.FishingEnter);
            if (Input.GetMouseButtonDown(0)) {
                UpdateStoryStep(StoryStep.Catch1);
            }
        }

        if (currentStoryStep == StoryStep.Catch1) {
            if (Catch1.GetComponent<FishingSpot>().collected) UpdateStoryStep(StoryStep.AfterConstellation1);
        }

        if (currentStoryStep == StoryStep.AfterConstellation1) {
            if (cam.cameraState != CameraState.Panning) {
                UpdateStoryStep(StoryStep.FishingWASDEnter);
            }
        }

        if (currentStoryStep == StoryStep.FishingWASDEnter) {
            if (GameManager.gameState == GameState.Fishing) UpdateStoryStep(StoryStep.FishingWASDFish);
            if (ReachFirstRune.GetComponent<InteractionCheckpoint>().interacted) UpdateStoryStep(StoryStep.Puzzle1);
        }

        if (currentStoryStep == StoryStep.FishingWASDFish) {
            if (GameManager.gameState != GameState.Fishing) UpdateStoryStep(StoryStep.FishingWASDEnter);
            if (ReachFirstRune.GetComponent<InteractionCheckpoint>().interacted) UpdateStoryStep(StoryStep.Puzzle1);
            if (FishingGameplay.I().isFishing && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow))) {
                UpdateStoryStep(StoryStep.ReachFirstRune);
            }
        }

        if (currentStoryStep == StoryStep.ReachFirstRune) {
            if (ReachFirstRune.GetComponent<InteractionCheckpoint>().interacted) UpdateStoryStep(StoryStep.Puzzle1);
        }

        if (currentStoryStep == StoryStep.Puzzle1) {
            if (puzzleCount == 1) {
                UpdateStoryStep(StoryStep.Catch2);
            }
        }

        if (currentStoryStep == StoryStep.Catch2) {
            if (Catch2.GetComponent<FishingSpot>().collected) UpdateStoryStep(StoryStep.AfterConstellation2);
        }

        if (currentStoryStep == StoryStep.AfterConstellation2) {
            if (cam.cameraState != CameraState.Panning) {
                UpdateStoryStep(StoryStep.ReachSecondRune);
            }
        }

        if (currentStoryStep == StoryStep.ReachSecondRune) {
            if (ReachSecondRune.GetComponent<InteractionCheckpoint>().interacted) UpdateStoryStep(StoryStep.Puzzle5);
        }

        if (currentStoryStep == StoryStep.Puzzle5) {
            if (puzzleCount == 5) {
                UpdateStoryStep(StoryStep.Catch3);
            }
        }

        if (currentStoryStep == StoryStep.Catch3) {
            if (Catch3.GetComponent<FishingSpot>().collected) UpdateStoryStep(StoryStep.AfterConstellation3);
        }

        if (currentStoryStep == StoryStep.AfterConstellation3) {
            if (cam.cameraState != CameraState.Panning) {
                UpdateStoryStep(StoryStep.ReachThirdRune);
            }
        }

        if (currentStoryStep == StoryStep.ReachThirdRune) {
            if (ReachThirdRune.GetComponent<InteractionCheckpoint>().interacted) UpdateStoryStep(StoryStep.Puzzle9);
        }

        if (currentStoryStep == StoryStep.Puzzle9) {
            if (puzzleCount == 9) {
                UpdateStoryStep(StoryStep.Catch4);
            }
        }
        
        if (currentStoryStep == StoryStep.Catch4) {
            if (Catch4.GetComponent<FishingSpot>().collected) UpdateStoryStep(StoryStep.AfterConstellation4);
        }

        if (currentStoryStep == StoryStep.AfterConstellation4) {
            if (cam.cameraState != CameraState.Panning) {
                UpdateStoryStep(StoryStep.ReachDoor);
            }
        }

        if (currentStoryStep == StoryStep.ReachDoor) {
            if (ReachDoor.GetComponent<InteractionCheckpoint>().interacted) {
                doorAnimator.SetTrigger("Open Door");
                UpdateStoryStep(StoryStep.ReachCompass);
            }
        }

        if (currentStoryStep == StoryStep.ReachCompass) {
            if (ReachCompass.GetComponent<InteractionCheckpoint>().interacted) UpdateStoryStep(StoryStep.Finished);
        }

        if (currentStoryStep == StoryStep.Finished) {
            if (cam.cameraState != CameraState.Panning) {
                cam.cameraState = CameraState.Stationary;
            }
        }
    }

    // used on changing the storystep
    public void UpdateStoryStep(StoryStep storyStep) {

        if (storyStep == StoryStep.StartGame) {
            cam.SaveValues();
            ClickAnywhereToBeginUI.SetActive(true);
            Camera.main.transform.position = TitleCam.transform.position;
            Camera.main.transform.rotation = TitleCam.transform.rotation;

            cam.cameraState = CameraState.Stationary;
        }
        else ClickAnywhereToBeginUI.SetActive(false);

        if (storyStep == StoryStep.StartGame || storyStep == StoryStep.StartGameAnimation) StartTitleUI.SetActive(true);
        else StartTitleUI.SetActive(false);

        if (storyStep == StoryStep.StartGamePanCamera) {
            cam.cameraPanningMovement.Add(new CameraPanningMovement(cam.lastPosition, cam.lastRotation, 4, 0));
            cam.StartPanning();
        }

        // if (storyStep == StoryStep.ZoomCamera) {
        //     ZoomUI.SetActive(true);
        //     Compass.SetActive(true);
        // }
        // else ZoomUI.SetActive(false);

        if (storyStep == StoryStep.WASDMovement) {
            WASDMovementUI.SetActive(true);
            Compass.SetActive(true);
        }
        else WASDMovementUI.SetActive(false);

        if (storyStep == StoryStep.FishingEnter) FishingEnterUI.SetActive(true);
        else FishingEnterUI.SetActive(false);

        if (storyStep == StoryStep.Fishing) FishingUI.SetActive(true);
        else FishingUI.SetActive(false);

        if (storyStep == StoryStep.AfterConstellation1) {
            Tinystars1.SetActive(true);
            Barrier1.SetActive(false);
            Constellation1.GetComponent<FadeIn>().fadingIn = true;
            cam.SaveValues();
            cam.cameraPanningMovement.Add(new CameraPanningMovement(Constellation1Cam.transform.position, Constellation1Cam.transform.rotation.eulerAngles, 2, 2));
            cam.cameraPanningMovement.Add(new CameraPanningMovement(TinyStars1Cam.transform.position, TinyStars1Cam.transform.rotation.eulerAngles, 2, 2));
            cam.cameraPanningMovement.Add(new CameraPanningMovement(cam.lastPosition, cam.lastRotation, 2, 0.1f));
            cam.StartPanning();
        }

        if (storyStep == StoryStep.FishingWASDEnter) FishingWASDEnterUI.SetActive(true);
        else FishingWASDEnterUI.SetActive(false);

        if (storyStep == StoryStep.FishingWASDFish) FishingWASDFishUI.SetActive(true);
        else FishingWASDFishUI.SetActive(false);

        if (storyStep == StoryStep.Puzzle1) {
            cam.SaveValues();
            cam.cameraPanningMovement.Add(new CameraPanningMovement(Rune1Cam.transform.position, Rune1Cam.transform.rotation.eulerAngles, 2, 2));
            cam.cameraPanningMovement.Add(new CameraPanningMovement(cam.lastPosition, cam.lastRotation, 2, 2));
            cam.StartPanning();
        }

        if (storyStep == StoryStep.Catch2) {
            Catch2.SetActive(true);
            cam.SaveValues();
            cam.cameraPanningMovement.Add(new CameraPanningMovement(Constellation2Cam.transform.position, Constellation2Cam.transform.rotation.eulerAngles, 2, 2));
            cam.cameraPanningMovement.Add(new CameraPanningMovement(cam.lastPosition, cam.lastRotation, 2, 2));
            cam.StartPanning();
        }

        if (storyStep == StoryStep.AfterConstellation2) {
            Tinystars2.SetActive(true);
            Barrier2.SetActive(false);
            Constellation2.GetComponent<FadeIn>().fadingIn = true;
            cam.SaveValues();
            cam.cameraPanningMovement.Add(new CameraPanningMovement(Constellation2Cam.transform.position, Constellation2Cam.transform.rotation.eulerAngles, 2, 2));
            cam.cameraPanningMovement.Add(new CameraPanningMovement(TinyStars2Cam.transform.position, TinyStars2Cam.transform.rotation.eulerAngles, 2, 2));
            cam.cameraPanningMovement.Add(new CameraPanningMovement(cam.lastPosition, cam.lastRotation, 2, 2));
            cam.StartPanning();
        }

        if (storyStep == StoryStep.Catch3) {
            Catch3.SetActive(true);
            cam.SaveValues();
            cam.cameraPanningMovement.Add(new CameraPanningMovement(Constellation3Cam.transform.position, Constellation3Cam.transform.rotation.eulerAngles, 2, 2));
            cam.cameraPanningMovement.Add(new CameraPanningMovement(cam.lastPosition, cam.lastRotation, 2, 2));
            cam.StartPanning();
        }

        if (storyStep == StoryStep.AfterConstellation3) {
            Tinystars3.SetActive(true);
            Barrier3.SetActive(false);
            Constellation3.GetComponent<FadeIn>().fadingIn = true;
            cam.SaveValues();
            cam.cameraPanningMovement.Add(new CameraPanningMovement(Constellation3Cam.transform.position, Constellation3Cam.transform.rotation.eulerAngles, 2, 2));
            cam.cameraPanningMovement.Add(new CameraPanningMovement(cam.lastPosition, cam.lastRotation, 2, 2));
            cam.StartPanning();
        }

        if (storyStep == StoryStep.Catch4) {
            Catch4.SetActive(true);
            cam.SaveValues();
            cam.cameraPanningMovement.Add(new CameraPanningMovement(Constellation4Cam.transform.position, Constellation4Cam.transform.rotation.eulerAngles, 2, 2));
            cam.cameraPanningMovement.Add(new CameraPanningMovement(cam.lastPosition, cam.lastRotation, 2, 2));
            cam.StartPanning();
        }

        if (storyStep == StoryStep.AfterConstellation4) {
            Constellation4.GetComponent<FadeIn>().fadingIn = true;
            cam.SaveValues();
            cam.cameraPanningMovement.Add(new CameraPanningMovement(Constellation4Cam.transform.position, Constellation4Cam.transform.rotation.eulerAngles, 2, 2));
            cam.cameraPanningMovement.Add(new CameraPanningMovement(cam.lastPosition, cam.lastRotation, 2, 2));
            cam.StartPanning();
        }

        if (storyStep == StoryStep.ReachCompass) {
            Islands.SetActive(true);
        }

        if (storyStep == StoryStep.Finished) {
            cam.cameraPanningMovement.Add(new CameraPanningMovement(EndingCam.transform.position, EndingCam.transform.rotation.eulerAngles, 2, 2));
        }

        currentStoryStep = storyStep;

    }
}
