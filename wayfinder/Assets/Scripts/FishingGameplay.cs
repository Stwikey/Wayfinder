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

    public GameObject lineRendererPrefab;

    public float fishingRange = 5.0f;

    public FishingType fishingType;

    GameObject currentLine;
    bool isFishing = false;
    GameObject fishingZone;

    void Start() {

    }

    void Update() {

        if (GameManager.gameState != GameState.Movement) return;

        Vector3 mousePos = Input.mousePosition;
        bool rightMouse = Input.GetMouseButton(1);
        bool rightmouseDown = Input.GetMouseButtonDown(1);
        bool rightmouseUp = Input.GetMouseButtonUp(1);

        // stopping fishing
        if (rightmouseUp || !rightMouse) {
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

        // if out of range
        if ((hitpos - transform.position).magnitude > fishingRange) {
            StopFishing();
            return;
        }

        // start fishing
        if (rightmouseDown) {
            // setting the type
            fishingType = currentType;
            StartFishing();
        }
        
        if (rightMouse && isFishing) {
            // if we change from zone to puzzle or puzzle to zone
            if (fishingType != currentType) {
                StopFishing();
                return;
            }

            FishDrawing fishDrawing = currentLine.GetComponent<FishDrawing>();

            // updating the points
            fishDrawing.AddPoint(hitpos);

            // adding the object to linerenderer
            if (hitFish != null) {
                fishDrawing.AddFish(hitFish);
                hitFish.GetComponent<FishingSpot>().Caught();
                // updating the fishingZone if there is one
                fishingZone.GetComponent<FishingZone>()?.CaughtFish(hitFish);
            }
        } 
    }

    public void StopFishing() {
        isFishing = false;
        if (currentLine != null) {
            currentLine.GetComponent<FishDrawing>().lineState = LineState.Destroy;
            fishingZone.GetComponent<FishingZone>()?.FinishedTrail();
        }
    }

    void StartFishing() {
        isFishing = true;
        currentLine = Object.Instantiate(lineRendererPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero));
    }
}
