using UnityEngine;

public class SkyboxController : MonoBehaviour {
    public Texture skyboxDark;
    public Texture skyboxBlue;
    public Texture skyboxGreen;
    public Texture skyboxPink;
    public Material material;

    public float skyboxDarkStart = 1;
    public float skyboxDarkEnd = 200;
    public float skyboxBlueStart = 400;
    public float skyboxBlueEnd = 627;
    public float skyboxGreenStart = 889;
    public float skyboxGreenEnd = 1150;
    public float skyboxPinkStart = 1259;
    public float skyboxPinkEnd = 1651;
    void Start() {
        RenderSettings.skybox = material;
    }

    // Update is called once per frame
    void Update() {
        float zPos = transform.position.z;
        if (zPos < skyboxDarkEnd) {
            material.SetTexture("_Texture1", skyboxDark);
            material.SetTexture("_Texture2", skyboxDark);
        }
        else if (zPos >= skyboxDarkEnd && zPos <= skyboxBlueStart) {
            material.SetTexture("_Texture1", skyboxDark);
            material.SetTexture("_Texture2", skyboxBlue);
            material.SetFloat("_Blend", (zPos - skyboxDarkEnd) / (skyboxBlueStart - skyboxDarkEnd));
        }
        else if (zPos > skyboxBlueStart && zPos < skyboxBlueEnd) {
            material.SetTexture("_Texture1", skyboxBlue);
            material.SetTexture("_Texture2", skyboxBlue);
        }
        else if (zPos >= skyboxBlueEnd && zPos <= skyboxGreenStart) {
            material.SetTexture("_Texture1", skyboxBlue);
            material.SetTexture("_Texture2", skyboxGreen);
            material.SetFloat("_Blend", (zPos - skyboxBlueEnd) / (skyboxGreenStart - skyboxBlueEnd));
        }
        else if (zPos > skyboxGreenStart && zPos < skyboxGreenEnd) {
            material.SetTexture("_Texture1", skyboxGreen);
            material.SetTexture("_Texture2", skyboxGreen);
        }
        else if (zPos >= skyboxGreenEnd && zPos <= skyboxPinkStart) {
            material.SetTexture("_Texture1", skyboxGreen);
            material.SetTexture("_Texture2", skyboxPink);
            material.SetFloat("_Blend", (zPos - skyboxGreenEnd) / (skyboxPinkStart - skyboxGreenEnd));
        }
        else {
            material.SetTexture("_Texture1", skyboxPink);
            material.SetTexture("_Texture2", skyboxPink);
        }
    }
}
