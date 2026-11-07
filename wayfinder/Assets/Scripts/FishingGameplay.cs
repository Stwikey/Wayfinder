using UnityEngine;
using System.Collections.Generic;

public enum FishingType {
    Zone,
    General
};

public class FishingGameplay : MonoBehaviour {

    // Singleton

    private static FishingGameplay _instance;

    private FishingGameplay() {
        _instance = this;
    }

    public static FishingGameplay I() {
        if (_instance == null) {
            FishingGameplay instance = new FishingGameplay();
            _instance = instance;
        }
        return _instance;
    }

    public GameObject fishingDrawLinePrefab;
    public GameObject fishingLinePrefab;

    public GameObject fishingTip;

    public float fishingRange = 5.0f;

    public FishingType fishingType;

    public Animator animator;
    GameObject currentLine;
    bool isFishing = false;
    GameObject fishingZone;
    FishingLine fishingLine;
    
    void Start() {
        GameObject obj = Object.Instantiate(fishingLinePrefab, Vector3.zero, Quaternion.Euler(0, 0, 0));
        fishingLine = obj.GetComponent<FishingLine>();
        fishingLine.startPoint = fishingTip;
    }

    void Update() {

        Vector3 mousePos = Input.mousePosition;
        bool rightMouse = Input.GetMouseButton(1);
        bool rightmouseDown = Input.GetMouseButtonDown(1);
        bool rightmouseUp = Input.GetMouseButtonUp(1);

        // exit fishing
        if (GameManager.gameState == GameState.Fishing && rightmouseDown) {
            StopFishing();
            return;
        }

        // enter fishing
        if (GameManager.gameState == GameState.Movement) {
            if (rightmouseDown) GameManager.gameState = GameState.Fishing;
        }

        // not in fishing
        if (GameManager.gameState != GameState.Fishing) {
            StopFishing();
            return;
        }

        bool leftMouse = Input.GetMouseButton(0);
        bool leftmouseDown = Input.GetMouseButtonDown(0);
        bool leftmouseUp = Input.GetMouseButtonUp(0);

        if (isFishing && leftmouseUp) {
            StopFishing();
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit[] rayHits = Physics.RaycastAll(ray);

        // if we hit something that is not a fish
        for (int i = 0; i < rayHits.Length; i++) {
            if (rayHits[i].collider.tag != "FishingZone" && rayHits[i].collider.tag != "SmallFish" && rayHits[i].collider.tag != "PuzzleFish" && rayHits[i].collider.tag != "Water") {
                StopFishing();
                return;
            }
        }

        // figure out if it is in the zone or not
        FishingType currentType = FishingType.General;
        Vector3 hitpos = Vector3.zero;

        GameObject hitFish = null;
        for (int i = 0; i < rayHits.Length; i++) {
            if (rayHits[i].collider.tag == "FishingZone") {
                currentType = FishingType.Zone;
                fishingZone = rayHits[i].collider.gameObject.transform.parent.gameObject;
            }
            if (rayHits[i].collider.tag == "Water") {
                hitpos = new Vector3(rayHits[i].point.x, 0.1f, rayHits[i].point.z);
            }
            if (rayHits[i].collider.tag == "SmallFish" || rayHits[i].collider.tag == "PuzzleFish") {
                hitFish = rayHits[i].collider.gameObject.transform.parent.gameObject;
            }
        }

        float distance = (new Vector3(hitpos.x - transform.position.x, 0, hitpos.z - transform.position.z)).magnitude;

        // if out of range just don't update
        if (distance > fishingRange) {
            if (leftmouseDown) return;
            Vector3 newAdd = new Vector3(hitpos.x - transform.position.x, 0, hitpos.z - transform.position.z);

            hitpos = new Vector3(newAdd.x / distance * fishingRange, hitpos.y, newAdd.z / distance * fishingRange) + new Vector3(transform.position.x, 0, transform.position.z);
        }

        // start fishing
        if (leftmouseDown) {
            // setting the type
            fishingType = currentType;
            StartFishing();
        }
        
        if (leftMouse && isFishing) {
            // if we change from zone to puzzle or puzzle to zone
            if (fishingType != currentType) {
                StopFishing();
                return;
            }

            FishDrawing fishDrawing = currentLine.GetComponent<FishDrawing>();

            // updating the points
            fishDrawing.AddPoint(hitpos);
            fishingLine.UpdateEndpoint(hitpos);

            // adding the object to linerenderer
            if (hitFish != null) {
                //AudioManager.I().PlaySFX(AudioType.Star1);
                fishDrawing.AddFish(hitFish);
                hitFish.GetComponent<FishingSpot>().Caught();
                // updating the fishingZone if there is one
                fishingZone?.GetComponent<FishingZone>()?.CaughtFish(hitFish);
            }
        } 
    }

    public void StopFishing() {
        if (!isFishing) return;
        
        animator.SetTrigger("Catch Rod");
        isFishing = false;
        fishingLine.UpdateFishingState(FishingLineState.Back);
        if (currentLine != null) {
            currentLine.GetComponent<FishDrawing>().lineState = LineState.Destroy;
            if (fishingType == FishingType.Zone) fishingZone.GetComponent<FishingZone>()?.FinishedTrail();
            else {
                currentLine.GetComponent<FishDrawing>().CollectFishes();
            }
        }

        GameManager.gameState = GameState.Movement;
    }

    void StartFishing() {
        animator.SetTrigger("Cast Rod");
        fishingLine.UpdateFishingState(FishingLineState.Cast);
        isFishing = true;
        currentLine = Object.Instantiate(fishingDrawLinePrefab, Vector3.zero, Quaternion.Euler(Vector3.zero));
    }
}
