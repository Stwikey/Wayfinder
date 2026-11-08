using UnityEngine;
using System.Collections.Generic;

public class FishingZone : MonoBehaviour {

    public List<GameObject> points;
    
    List<GameObject> currentOrder;

    public GameObject Rune;
    public Material GlowingMaterial;
    public int glowingIdx;

    void Start() {
        currentOrder = new List<GameObject>();
    }

    void Update() {

    }

    public void FinishedTrail() {
        bool completed = true;
        if (currentOrder.Count != points.Count) completed = false;
        else {
            for (int i = 0; i < points.Count; i++) {
                if (points[i] != currentOrder[i]) completed = false;
            }
        }
        
        for (int i = 0; i < points.Count; i++) {
            if (completed) points[i].GetComponent<FishingSpot>().Collect();
            else {
                points[i].GetComponent<FishingSpot>().Reset();
            }
        }

        if (completed) {
            var mats = Rune.GetComponent<MeshRenderer>().materials;
            mats[glowingIdx] = GlowingMaterial;
            Rune.GetComponent<MeshRenderer>().materials = mats;
            StoryManager.I().puzzleCount++;
            AudioManager.I().PlaySFX(AudioType.Chime);
            gameObject.SetActive(false);
        }

        currentOrder = new List<GameObject>();
    }

    public void CaughtFish(GameObject hitFish) {

        for (int i = 0; i < points.Count; i++) {
            if (hitFish == points[i]) {
                currentOrder.Add(points[i]);
            }
        }

    }

}
