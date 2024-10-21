using UnityEngine;

public class GazeController : MonoBehaviour
{
    public Transform target;  // The target that the character should look at
    public Transform lookJoint;  // The joint 

    void Update()
    {
        if (target == null || lookJoint == null) return;  

        Vector3 r = lookJoint.position - target.position;  
        Vector3 e = lookJoint.forward;  

        Vector3 cross = Vector3.Cross(r, e); 
        float normCross = cross.magnitude; 

        float dot = Vector3.Dot(r, e);  

        // Calculate the rotation angle (ϕ)
        float angle = Mathf.Atan2(normCross, Vector3.Dot(r, r) + dot) * Mathf.Rad2Deg;
        if (angle < 0.1f) return;
        Vector3 rotationAxis = cross.normalized;

        Quaternion rotation = Quaternion.AngleAxis(angle, rotationAxis);

        // because I use a prefab character which already did the "parent" step, I will apply the rotation to the lookjoint itself
        lookJoint.rotation = rotation * lookJoint.rotation;
        Debug.DrawLine(lookJoint.position, target.position, Color.green);
    }
}
