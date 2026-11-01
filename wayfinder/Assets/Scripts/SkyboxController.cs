using UnityEngine;

public class SkyboxController : MonoBehaviour {
    public Material skyboxBlue;
    public Material skyboxGreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        //RenderSettings.skybox = material.Lerp(skyboxBlue, skyboxGreen, 0.0f);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
