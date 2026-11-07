using UnityEngine;

public class FishingSpot : MonoBehaviour {
    
    public GameObject ripple;
    public GameObject star;

    Vector3 starPosition;

    public bool collected = false;

    void Start() {
        starPosition = star.transform.position;
    }

    public void Reset() {
        ripple.SetActive(true);
        star.transform.position = starPosition;
    }

    public void Caught() {
        ripple.SetActive(false);
    }
    
    public void Collect() {
        star.SetActive(false);
        collected = true;
    }
}
