using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour {
    public Slider bg;
    public Slider SFX;
    public Button exitButton;

    public GameObject SettingsObj;

    void Start() {

        exitButton.onClick.AddListener(() => {
            GameManager.gameState = GameState.Movement;
        });
    }

    void Update() {
        AudioManager.I().AudioVolume[0] = bg.value;
        AudioManager.I().AudioVolume[1] = SFX.value;
        if (GameManager.gameState == GameState.Settings) SettingsObj.SetActive(true);
        else SettingsObj.SetActive(false);
    }
}