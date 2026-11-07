using UnityEngine;

public class PlayerGameplay : MonoBehaviour {

    public Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    void Update() {
        if (GameManager.gameState == GameState.Fishing) Cursor.visible = true;
        else Cursor.visible = false;
    }
}
