using System;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType {
    Star1,
    Star2
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

    public void PlayBackground(AudioType audio) {
        AudioSources[0].volume = AudioVolume[0];
        AudioSources[0].clip = audioList[(int)audio];
        AudioSources[0].Play();
    }

    public void PlaySFX(AudioType audio) {
        for (int i = 1; i < AudioSources.Count; i++) {
            if (!AudioSources[i].isPlaying) {
                AudioSources[i].volume = AudioVolume[1];
                AudioSources[i].clip = audioList[(int)audio];
                AudioSources[i].Play();
                return;
            }
        }
    }
}