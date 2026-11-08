using UnityEngine;

public class PlayerGameplay : MonoBehaviour {

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.gameState == GameState.Movement) {
            GameManager.gameState = GameState.Settings;
        }
        if (GameManager.gameState == GameState.Fishing || GameManager.gameState == GameState.Settings) Cursor.visible = true;
        else Cursor.visible = false;
    }
}
