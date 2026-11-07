using UnityEngine;

public class FadeIn : MonoBehaviour {
    
    public float FadeInTime;
    public bool fadingIn = false;
    float elapsedTime = 0;

    void Update() {

        if (!fadingIn) return;

        float deltaTime = Time.deltaTime;
        elapsedTime += deltaTime;
        elapsedTime = Mathf.Min(elapsedTime, FadeInTime);
        float fraction = elapsedTime / FadeInTime;
        
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, fraction);
            
        if (fraction >= 1) fadingIn = false;
    }
}