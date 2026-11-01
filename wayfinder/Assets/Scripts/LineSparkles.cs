using UnityEngine;
using System.Collections.Generic;

public enum LineState {
    Drawing,
    Destroy
};

public class LineSparkles : MonoBehaviour {

    public LineState lineState;
    public GameObject sparkles;
    public List<Vector3> points;

    public List<GameObject> sparklesList;
    LineRenderer lineRenderer;
    
    
    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        sparklesList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update() {
        if (lineState == LineState.Destroy) DestroyLine();
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

    float timePassed = 0;
    void DestroyLine() {
        for (int i = 0; i < sparklesList.Count; i++) Destroy(sparklesList[i]);
        timePassed += Time.deltaTime;
        while (timePassed > 0.02f) {
            if (points.Count == 0) {
                break;
            }
            // if (sparklesList.size() != 0) {
            //     while (points)
            // }
            points.RemoveAt(0);
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
            timePassed -= 0.02f;
            
        }
        if (points.Count == 0) {
            Destroy(gameObject);
        }
    }

}
