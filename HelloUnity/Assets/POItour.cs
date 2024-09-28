using UnityEngine;

public class POItour : MonoBehaviour
{
    public Transform[] pointsOfInterest;  // Array of POIs
    public float speed = 5.0f;  
    private int currentPOI = 0;  
    private bool isMoving = false;  // State to check if the camera is currently moving between POIs
    private Vector3 startPosition;  
    private Quaternion startRotation;  
    private float t = 0.0f; 
    private float totalTime = 0.0f;  

    // Reference to the Main Camera
    private Transform cameraTransform;
    // Reference to Flythrough Camera script
    private Flythrough flythroughScript; 

    void Start()
    {
        cameraTransform = Camera.main.transform;
        flythroughScript = cameraTransform.GetComponent<Flythrough>();

        // Set the camera position and rotation to the first POI
        if (pointsOfInterest.Length > 0)
        {
            cameraTransform.position = pointsOfInterest[0].position;
            cameraTransform.rotation = pointsOfInterest[0].rotation;  
            flythroughScript.SyncRotationWithCurrentView(cameraTransform.rotation);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && !isMoving)
        {
            MoveToNextPOI();
        }

        if (isMoving)
        {
            if (flythroughScript != null && flythroughScript.enabled)
            {
                flythroughScript.enabled = false;
            }

            t += Time.deltaTime / totalTime;

            // Lerp position
            cameraTransform.position = Vector3.Lerp(startPosition, pointsOfInterest[currentPOI].position, t);
            
            // Slerp rotation 
            cameraTransform.rotation = Quaternion.Slerp(startRotation, pointsOfInterest[currentPOI].rotation, t);

            if (t >= 1.0f)
            {
                isMoving = false;
                if (flythroughScript != null)
                {
                    flythroughScript.SyncRotationWithCurrentView(cameraTransform.rotation);
                    flythroughScript.enabled = true;
                }
            }
        }
    }

    void MoveToNextPOI()
    {
        // Increment the POI index
        currentPOI = (currentPOI + 1) % pointsOfInterest.Length;

        startPosition = cameraTransform.position;
        startRotation = cameraTransform.rotation;

        float distance = Vector3.Distance(startPosition, pointsOfInterest[currentPOI].position);
        totalTime = distance / speed;

        // Reset t
        t = 0.0f;
        isMoving = true;

        if (flythroughScript != null)
        {
            flythroughScript.enabled = false;
        }
    }
}





