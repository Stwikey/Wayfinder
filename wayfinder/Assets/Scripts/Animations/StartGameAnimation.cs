using UnityEngine;

public class StartGameAnimation : MonoBehaviour {

    public GameObject TitleWithLinesObject;

    public float FadeInTime;
    public float WaitTime;

    bool fadingIn = false;
    bool waiting = false;
    float elapsedTime = 0;

    void Start() {

    }

    void Update() {

        if (!fadingIn && !waiting) return;

        float deltaTime = Time.deltaTime;
        elapsedTime += deltaTime;
        elapsedTime = Mathf.Min(elapsedTime, FadeInTime);
        float fraction = elapsedTime / FadeInTime;
        
        if (fadingIn) {
            
            TitleWithLinesObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, fraction);
            
            if (fraction >= 1) {
                fadingIn = false;
                elapsedTime = 0;
                waiting = true;
            }
        }
        else if (waiting) {
            if (elapsedTime > WaitTime) {
                StoryManager.I().UpdateStoryStep(StoryStep.StartGamePanCamera);
            }
        }
    }

    public void StartAnimation() {
        fadingIn = true;
    }

};

