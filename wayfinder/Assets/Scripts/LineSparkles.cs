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

    List<GameObject> sparklesList;
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
    }

    float timePassed = 0;
    void DestroyLine() {
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
