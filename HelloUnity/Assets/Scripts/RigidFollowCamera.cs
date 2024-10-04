using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidFollowCamera : MonoBehaviour
{
    public Transform target;

    public float horizontalDistance = 5f;
    public float verticalDistance = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null) {
            Vector3 tPos = target.position;
            Vector3 tUp = target.up;
            Vector3 tForward = target.forward;

            Vector3 cameraPos = tPos - tForward * horizontalDistance + tUp * verticalDistance;
            Vector3 lookVec = tPos - cameraPos;

            transform.position = cameraPos;
            transform.rotation = Quaternion.LookRotation(lookVec);
        }
    }
}
