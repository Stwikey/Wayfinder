using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour {

    public enum UIAnimationStep {
        Move,
        ChangeSprite,
        Wait
    };

    [Serializable]
    public struct UIAnimationInstruction {
        public UIAnimationStep animationStep;
        public float x; // if animationstep is move
        public float y; // if animationstep is move
        public float time; // if animation step is wait or move
        public int spriteIdx; // if animationstep is changesprite
    };

    public List<GameObject> sprites;

    public List<UIAnimationInstruction> instructions;

    void Start() {
        nextInstructionSetup();
    }

    public int curIdx = 0;
    float timePassed = 0;
    float moveX = 0, moveY = 0;

    void Update() {
        
        float deltaTime = Time.deltaTime;

        bool nextInstruction = false;

        UIAnimationInstruction curInstruction = instructions[curIdx];

        switch (curInstruction.animationStep) {
            case UIAnimationStep.Wait: {
                timePassed += deltaTime;
                if (timePassed >= curInstruction.time) nextInstruction = true;
                break;
            }
            case UIAnimationStep.ChangeSprite: {
                for (int i = 0; i < sprites.Count; i++) sprites[i].SetActive(false);
                sprites[curInstruction.spriteIdx].SetActive(true);
                nextInstruction = true;
                break;
            }
            case UIAnimationStep.Move : {
                float moveAmount = Mathf.Min(curInstruction.time - timePassed, deltaTime);
                transform.position = new Vector3(transform.position.x + moveX * moveAmount, transform.position.y + moveY * moveAmount, transform.position.z);
                timePassed += deltaTime;
                if (timePassed >= curInstruction.time) nextInstruction = true;
                break;
            }
            default: {
                Debug.Log("this is unexpected, ui animation");
                break;
            }
        }

        if (nextInstruction) {
            curIdx++;
            if (curIdx >= instructions.Count) curIdx = 0;
            nextInstructionSetup();
        }
        
    }

    void nextInstructionSetup() {

        UIAnimationInstruction newInstruction = instructions[curIdx];
            
        switch (newInstruction.animationStep) {
            case UIAnimationStep.Wait: {
                timePassed = 0;
                break;
            }
            case UIAnimationStep.Move : {
                timePassed = 0;
                moveX = (newInstruction.x - transform.position.x) / newInstruction.time;
                moveY = (newInstruction.y - transform.position.y) / newInstruction.time;
                break;
            }
            default:
                break;
        }
    }
}
