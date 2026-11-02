using UnityEngine;
using System.Collections.Generic;

public class FishingZone : MonoBehaviour {

    public Camera cam;

    public List<GameObject> points;
    public List<GameObject> pointStars;

    public GameObject player;

    public GameObject lineRendererPrefab;
    public GameObject currentLine;

    int idx = 0;
    int onIdx = -1;

    void Start() {
        
    }

    void Update() {

        if (GameManager.gameState != GameState.Fishing) return;

        Vector3 mousePos = Input.mousePosition;
        bool rightMouse = Input.GetMouseButton(1);
        bool rightmouseDown = Input.GetMouseButtonDown(1);
        bool rightmouseUp = Input.GetMouseButtonUp(1);
        
        

        if (rightmouseUp) TrailOff();

        if (rightMouse) {

            Ray ray = cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out var hit)) {

                Vector3 hitpos = new Vector3(hit.point.x, 0.1f, hit.point.z);

                if (rightmouseDown) {
                    TrailOn();
                }
                else {

                    currentLine.GetComponent<LineSparkles>().AddPoint(hitpos);

                    // updating stars
                    if (idx != 0) {
                        List<GameObject> p = currentLine.GetComponent<LineSparkles>().sparklesList;
                        if (idx >= 1) {
                            pointStars[0].transform.position = p[p.Count - 1].transform.position;
                        }
                        if (idx >= 2) {
                            pointStars[1].transform.position = p[p.Count - 2].transform.position;
                        }
                    }
                    else {
                        for (int i = 0; i < points.Count; i++) {
                            pointStars[i].transform.position = points[i].transform.position;
                        }
                    }
                }

                // currently on a point
                if (onIdx != -1) {
                    if (hit.collider.gameObject != points[onIdx]) {
                        onIdx = -1;
                        idx++;
                    }
                }

                for (int i = 0; i < points.Count; i++) {
                    if (hit.collider.gameObject == points[i]) {
                        if (i == idx) onIdx = idx;
                        // wrong point, reset
                        else TrailOff();
                    }
                }
            }
        } 
    }

    void TrailOff() {
        currentLine.GetComponent<LineSparkles>().lineState = LineState.Destroy;
        idx = 0;
        onIdx = -1;
    }

    void TrailOn() {
        currentLine = Object.Instantiate(lineRendererPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero));
    }

}
