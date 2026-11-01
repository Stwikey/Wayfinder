using UnityEngine;
using System.Collections.Generic;

public class FishingZone : MonoBehaviour {

    public Camera cam;
    public List<GameObject> points;
    public GameObject player;
    //public TrailRenderer trailRenderer;
    public GameObject lineRendererPrefab;
    //public GameObject sparkles;
    public GameObject currentLine;

    //List<GameObject> sparklesList;

    int idx = 0;
    int onIdx = 0;

    void Start() {
        //sparklesList = new List<GameObject>();
    }

    Vector3 lastPos = Vector3.zero;
    void Update() {

        Vector3 mousePos = Input.mousePosition;
        bool rightMouse = Input.GetMouseButton(1);
        bool rightmouseDown = Input.GetMouseButtonDown(1);
        bool rightmouseUp = Input.GetMouseButtonUp(1);
        
        Ray ray = cam.ScreenPointToRay(mousePos);

        if (rightmouseUp) currentLine.GetComponent<LineSparkles>().lineState = LineState.Destroy; //TrailOff();

        if (rightMouse) {

            if (Physics.Raycast(ray, out var hit)) {

                Vector3 hitpos = hit.point;
                hitpos = new Vector3(hitpos.x, 0.1f, hitpos.z);

                if (rightmouseDown) {
                    currentLine = Object.Instantiate(lineRendererPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero));
                }
                    //TrailOn(hitpos);
                else {
                    // if ((hitpos - lastPos).magnitude > 0.05f) {
                    //     GameObject obj = Object.Instantiate(sparkles, hitpos, Quaternion.Euler(Vector3.zero));
                    //     sparklesList.Add(obj);
                    // }

                    lastPos = hitpos;
                    currentLine.GetComponent<LineSparkles>().AddPoint(hitpos);

                    // moving trailRenderer
                   // trailRenderer.transform.position = hitpos;
                }

                if (onIdx != -1) {
                    if (hit.collider.gameObject != points[onIdx]) {
                        onIdx = -1;
                        idx++;
                    }
                }

                for (int i = 0; i < points.Count; i++) {
                    if (hit.collider.gameObject == points[i]) {
                        if (i == idx) {
                            onIdx = idx;
                            //if (idx == points.Count - 1) Debug.Log("ok maam");
                        }
                        else {
                            //Debug.Log(i);
                            //TrailOff();
                            idx = 0;
                        }
                    }
                }
                
            }
        } 
    }

    // void TrailOn(Vector3 position) {
    //     trailRenderer.Clear();
    //     trailRenderer.transform.position = position;
    //     trailRenderer.time = 1000;
    //     trailRenderer.emitting = true;
    // }

    // void TrailOff() {
    //     trailRenderer.emitting = false;
    //     trailRenderer.Clear();
    //     //trailRenderer.time = 0;

    //     for (int i = 0; i < sparklesList.Count; i++) Destroy(sparklesList[i]);
    //     sparklesList.Clear();
    // }
}
