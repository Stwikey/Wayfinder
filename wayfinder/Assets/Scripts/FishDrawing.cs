using UnityEngine;
using System.Collections.Generic;

public enum LineState {
    Drawing,
    Destroy
};

public class FishDrawing : MonoBehaviour {

    public LineState lineState;
    public GameObject sparkles;
    public List<Vector3> points;

    public List<GameObject> sparklesList;
    LineRenderer lineRenderer;

    List<GameObject> fishes;
    List<int> fishIdx;
    
    void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        sparklesList = new List<GameObject>();
        fishes = new List<GameObject>();
        fishIdx = new List<int>();

        // first item of fishes is always null to leave a space between mouse and other stars
        fishes.Add(null);
        fishIdx.Add(0);
    }

    // Update is called once per frame
    void Update() {
        
        if (lineState == LineState.Destroy) {
            DestroyLine();
            return;
        }

        // moving up the fish if they are at least 10 away
        fishIdx[0] = points.Count;
        for (int i = 1; i < fishIdx.Count; i++) {
            if (fishIdx[i - 1] - fishIdx[i] >= 10) {
                fishIdx[i]++;
                fishes[i].transform.position = points[fishIdx[i]];
            }
            if (fishIdx[i - 1] - fishIdx[i] >= 20) {
                fishIdx[i]++;
                fishes[i].transform.position = points[fishIdx[i]];
            }
        }
    }

    public void AddPoint(Vector3 position) {
        if (points.Count == 0 || (points[points.Count - 1] - position).magnitude > 0.1f) {
            points.Add(position);
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
        }
        if (sparklesList.Count == 0 || (sparklesList[sparklesList.Count - 1].transform.position - position).magnitude > 0.5f) {
            GameObject tmp = Object.Instantiate(sparkles, position, Quaternion.Euler(Vector3.zero));
            sparklesList.Add(tmp);
        }
    }

    public void AddFish(GameObject fish) {
        // this would be the spot
        fishes.Add(fish.GetComponent<FishingSpot>().star);
        fishIdx.Add(points.Count);
    }

    float timePassed = 0;
    void DestroyLine() {
        for (int i = 0; i < sparklesList.Count; i++) Destroy(sparklesList[i]);
        timePassed += Time.deltaTime;
        while (timePassed > 0.005f) {
            if (points.Count == 0) {
                break;
            }
            points.RemoveAt(0);
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
            timePassed -= 0.005f;
            
        }
        if (points.Count == 0) {
            Destroy(gameObject);
        }
    }

    public void CollectFishes() {
        if (fishes.Count > 1) AudioManager.I().PlaySFX(AudioType.StarObtain);
        for (int i = 1; i < fishes.Count; i++) {
            fishes[i].transform.parent.gameObject.GetComponent<FishingSpot>().Collect();
        }
    }

}
