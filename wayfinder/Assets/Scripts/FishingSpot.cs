using UnityEngine;

public class FishingSpot : MonoBehaviour {
    
    public GameObject ripple;
    public GameObject star;

    Vector3 starPosition;

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
}
