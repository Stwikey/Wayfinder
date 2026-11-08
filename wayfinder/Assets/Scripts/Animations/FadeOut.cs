using UnityEngine;

public class FadeOut : MonoBehaviour {
    
    public float FadeOutTime;
    public bool fadingOut = false;
    float elapsedTime = 0;

    void Update() {

        if (!fadingOut) return;

        float deltaTime = Time.deltaTime;
        elapsedTime += deltaTime;
        elapsedTime = Mathf.Min(elapsedTime, FadeOutTime);
        float fraction = elapsedTime / FadeOutTime;
        
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - fraction);
            
        if (fraction >= 1) fadingOut = false;
    }
}