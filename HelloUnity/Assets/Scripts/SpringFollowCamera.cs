using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringFollowCamera : MonoBehaviour
{
    // Public parameters to be adjust in Unity
    public Transform target; 
    public float horizontalDistance = 5f; 
    public float verticalDistance = 3f;  
    public float dampConstant = 3f; 
    public float springConstant = 5f; 

    // Private variables 
    private Vector3 velocity = Vector3.zero; // The current velocity of the camera
    private Vector3 actualPosition; 

    void Start()
    {
        actualPosition = transform.position;
    }

    // LateUpdate is called once per frame, after all Update() calls
    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 tPos = target.position;
            Vector3 tUp = target.up;
            Vector3 tForward = target.forward;

            Vector3 idealEye = tPos - tForward * horizontalDistance + tUp * verticalDistance;

            Vector3 cameraForward = tPos - actualPosition;

            Vector3 displacement = actualPosition - idealEye;
            Vector3 springAccel = (-springConstant * displacement) - (dampConstant * velocity);

            // Update the camera's velocity based on the spring acceleration
            velocity += springAccel * Time.deltaTime;
            actualPosition += velocity * Time.deltaTime;

            transform.position = actualPosition;
            transform.rotation = Quaternion.LookRotation(cameraForward);
        }
        else
        {
            Debug.LogWarning("No target assigned");
        }
    }
}


