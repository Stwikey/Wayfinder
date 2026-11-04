using UnityEngine;
using System.Collections.Generic;

public class FishingZone : MonoBehaviour {

    public List<GameObject> points;

    int idx = 0;

    void Start() {
        
    }

    void Update() {

    }

    public void FinishedTrail() {
        if (idx != points.Count) {
            for (int i = 0; i < points.Count; i++) {
                points[i].GetComponent<FishingSpot>().Reset();
            }
            idx = 0;
        }
    }

    public void CaughtFish(GameObject hitFish) {

        for (int i = 0; i < points.Count; i++) {
            if (hitFish == points[i]) {
                if (i == idx) idx++;
                // wrong point, reset
                else {
                    FishingGameplay.I().StopFishing();
                    FinishedTrail();
                }
            }
        }

    }

}
