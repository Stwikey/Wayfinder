using UnityEngine;

public class PlayerGameplay : MonoBehaviour {

    public Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    void Update() {
        // Vector3 mousePos = Input.mousePosition;
        // bool rightmouseDown = Input.GetMouseButtonDown(1);
    
        // if (GameManager.gameState == GameState.Movement && rightmouseDown) {
        //     Ray ray = cam.ScreenPointToRay(mousePos);
        //     RaycastHit[] rayHits = Physics.RaycastAll(ray);

        //     for (int i = 0; i < rayHits.Length; i++) {
        //         if (rayHits[i].collider.tag == "FishingZone") {
        //             cam.enabled = false;
        //             rayHits[i].collider.gameObject.transform.parent.gameObject.GetComponent<FishingZone>().cam.enabled = true;
        //             GameManager.gameState = GameState.Fishing;
        //             break;
        //         }
        //     }
        // }
    }
}
