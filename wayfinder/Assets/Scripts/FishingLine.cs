using UnityEngine;

public enum FishingLineState {
    Cast,
    Back,
    Fishing,
    Hide,
};

public class FishingLine : MonoBehaviour {
    public AnimationCurve fishingLine;
    public GameObject startPoint;
    public Vector3 endPoint;
    public Vector3 endPointWanted;

    FishingLineState fishingLineState = FishingLineState.Hide;

    public float ReelSpeed;
    
    LineRenderer lineRenderer;

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update() {
        float deltaTime = Time.deltaTime;

        if (fishingLineState == FishingLineState.Back) {
            endPointWanted = startPoint.transform.position;
        }

        Vector3 left = endPointWanted - endPoint;
        if (left.magnitude < ReelSpeed * deltaTime) {
            endPoint = endPointWanted;
            if (fishingLineState == FishingLineState.Back) {
                fishingLineState = FishingLineState.Hide;
            }
            if (fishingLineState == FishingLineState.Back) {
                fishingLineState = FishingLineState.Fishing;
            }
        }
        else endPoint = endPoint + left * ReelSpeed * deltaTime;

        if (fishingLineState != FishingLineState.Hide) SetLine();
        else lineRenderer.positionCount = 0;
    }

    public void UpdateFishingState(FishingLineState newState) {
        if (newState == FishingLineState.Cast) {
            endPoint = startPoint.transform.position;
        }
        if (newState == FishingLineState.Back) {
            endPointWanted = startPoint.transform.position;
        }
        fishingLineState = newState;
    }

    public void UpdateEndpoint(Vector3 newPosition) {
        endPointWanted = newPosition;
    }

    void SetLine() {
        Vector3 startPosition = startPoint.transform.position;
        Vector3 withoutY = new Vector3(endPoint.x - startPosition.x, 0, endPoint.z - startPosition.z);
        int numPoints = (int)(withoutY.magnitude / 0.2f);

        float moveX = (endPoint.x - startPosition.x) / numPoints;
        float moveZ = (endPoint.z - startPosition.z) / numPoints;
        float yDiff = (endPoint.y - startPosition.y);
        lineRenderer.positionCount = numPoints;
        for (int i = 0; i < numPoints; i++) {
            float xPos = startPosition.x + moveX * i;
            float zPos = startPosition.z + moveZ * i;
            float yPos = Mathf.Lerp(endPoint.y, startPosition.y, fishingLine.Evaluate((float)i / (float)numPoints));
            lineRenderer.SetPosition(i, new Vector3(xPos, yPos, zPos));
        }
    }
}