using UnityEngine;

public enum GameState {
    Movement,
    Fishing,
    CameraMovement,
    Settings
};

public class GameManager {

    public static GameState gameState = GameState.Movement;
    public static GameState nextGameState;

}
