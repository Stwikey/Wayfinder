using System;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType {
    Area1,
    Area2,
    Area3,
    Area4,
    Chime,
    Gateway,
    Ripple,
    Sloshing,
    StarObtain,
    Star1,
    Star2,
    ThemeMusic,
    Waves
};

public class AudioManager : MonoBehaviour {

    // Singleton

    private static AudioManager _instance;

    private AudioManager() {
        _instance = this;
    }

    public static AudioManager I() {
        if (_instance == null) {
            AudioManager instance = new AudioManager();
            _instance = instance;
        }
        return _instance;
    }

    // for the inspector
    [Serializable]
    public struct AudioInspectorStruct {
        public AudioType audioType;
        public AudioClip audioClip;
    }

    // inspector fields
    public List<AudioInspectorStruct> AudioListInspector;
    private List<AudioClip> audioList;

    // use first source for background music only
    public List<AudioSource> AudioSources;

    // player settings
    public List<float> AudioVolume;
    
    void Start() {
        audioList = new List<AudioClip>();
        for (int i = 0; i < AudioListInspector.Count; i++) audioList.Add(null);
        for (int i = 0; i < AudioListInspector.Count; i++) {
            audioList[(int)AudioListInspector[i].audioType] = AudioListInspector[i].audioClip;
        }
    }

    void Update() {
        if (!AudioSources[0].isPlaying) {
            if (StoryManager.I().currentStoryStep < StoryStep.AfterConstellation2) {
                AudioManager.I().PlayBackground(AudioType.Area1);
            }
            else if (StoryManager.I().currentStoryStep < StoryStep.AfterConstellation3) {
                AudioManager.I().PlayBackground(AudioType.Area2);
            }
            else if (StoryManager.I().currentStoryStep < StoryStep.AfterConstellation4) {
                AudioManager.I().PlayBackground(AudioType.Area3);
            }
            else AudioManager.I().PlayBackground(AudioType.Area4);
        }
        AudioManager.I().PlaySFX(AudioType.Waves, true);

        AudioSources[0].volume = AudioVolume[0];

        for (int i = 1; i < AudioSources.Count; i++) {
            if (AudioSources[i].isPlaying) {
                if (AudioSources[i].clip == audioList[(int)AudioType.Waves] || AudioSources[i].clip == audioList[(int)AudioType.Sloshing]) AudioSources[i].volume = AudioVolume[1] * 0.5f;
                else AudioSources[i].volume = AudioVolume[1];
            }
        }
    }

    public void PlayBackground(AudioType audio) {
        AudioSources[0].volume = AudioVolume[0];
        AudioSources[0].clip = audioList[(int)audio];
        AudioSources[0].Play();
    }

    public void StopSFX(AudioType audio) {
        for (int i = 1; i < AudioSources.Count; i++) {
            if (AudioSources[i].isPlaying && AudioSources[i].clip == audioList[(int)audio]) AudioSources[i].Stop();
        }
    }

    public void PlaySFX(AudioType audio, bool yield = false) {
        if (yield) {
            for (int i = 1; i < AudioSources.Count; i++) {
                if (AudioSources[i].isPlaying && AudioSources[i].clip == audioList[(int)audio]) return;
            }
        }
        for (int i = 1; i < AudioSources.Count; i++) {
            if (!AudioSources[i].isPlaying) {
                if (audio == AudioType.Waves || audio == AudioType.Sloshing) AudioSources[i].volume = AudioVolume[1] * 0.5f;
                else AudioSources[i].volume = AudioVolume[1];
                AudioSources[i].clip = audioList[(int)audio];
                AudioSources[i].Play();
                return;
            }
        }
    }
}