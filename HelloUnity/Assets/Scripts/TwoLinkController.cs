using UnityEngine;

public class TwoLinkController : MonoBehaviour
{
    public Transform target; 
    public Transform endEffector;  

    private Transform middleJoint; 
    private Transform baseJoint;  

    void Start()
    {
        middleJoint = endEffector.parent;
        baseJoint = middleJoint.parent;
    }

    void Update()
    {
        if (target == null || endEffector == null) return;

        // Calculate the distances
        float upperArmLength = Vector3.Distance(baseJoint.position, middleJoint.position);  // Shoulder to elbow
        float lowerArmLength = Vector3.Distance(middleJoint.position, endEffector.position);  // Elbow to hand
        float targetDistance = Vector3.Distance(baseJoint.position, target.position);  // Shoulder to target

        // Clamp target distance to ensure it's within the arm's total length
        targetDistance = Mathf.Min(targetDistance, upperArmLength + lowerArmLength);

        Vector3 baseToMiddle = middleJoint.position - baseJoint.position;
        Vector3 middleToTarget = target.position - middleJoint.position;
        Vector3 baseToEnd = target.position - baseJoint.position;
        Vector3 endToTargetEffector = target.position - endEffector.position;
        float l1 = baseToMiddle.magnitude;
        float l2 = (endEffector.position - middleJoint.position).magnitude;
        float r = baseToEnd.magnitude;

        Vector3 middleRotationAxis = Vector3.Cross(baseToMiddle, middleToTarget).normalized;

        Vector3 baseRotationAxis = Vector3.Cross(baseToEnd, endToTargetEffector).normalized;

        float cosTheta = Mathf.Clamp((l1 * l1 + l2 * l2 - r * r) / (2 * l1 * l2), -1f, 1f);
        float theta = Mathf.Acos(cosTheta) * Mathf.Rad2Deg; 

        Vector3 crossProduct = Vector3.Cross(baseToEnd, endToTargetEffector); 
        float dotSum = Vector3.Dot(baseToEnd, baseToEnd) + Vector3.Dot(baseToEnd, endToTargetEffector);  
        float phi = Mathf.Atan2(crossProduct.magnitude, dotSum) * Mathf.Rad2Deg; 

        middleJoint.rotation = Quaternion.AngleAxis(180 - theta, middleRotationAxis) * baseJoint.rotation;  
        baseJoint.rotation = Quaternion.AngleAxis(phi, baseRotationAxis) * baseJoint.rotation; 

        // Debug lines to visualize arm positions
        Debug.DrawLine(baseJoint.position, middleJoint.position, Color.red);  
        Debug.DrawLine(middleJoint.position, endEffector.position, Color.blue);  
        Debug.DrawLine(endEffector.position, target.position, Color.green); 

        float grandparentToEndEffectorDistance = Vector3.Distance(baseJoint.position, endEffector.position);
        float grandparentToTargetDistance = Vector3.Distance(baseJoint.position, target.position);

        // Print the calculated distances
        Debug.Log("The first distance should be smaller or the same: " + grandparentToEndEffectorDistance + " and " + grandparentToTargetDistance);
    }
}



