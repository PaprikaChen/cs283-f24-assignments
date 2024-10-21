using UnityEngine;

public class TwoLinkController : MonoBehaviour
{
    public Transform target;  // The target that the end effector should move towards
    public Transform endEffector;  // The end effector (e.g., hand)

    private Transform middleJoint;  // Middle joint (e.g., elbow)
    private Transform baseJoint;  // Base joint (e.g., shoulder)

    void Start()
    {
        // Cache the middle and base joints (elbow and shoulder) for easier access
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

        // Step 1: Compute the shoulder-to-target direction and move the end effector to the target
        Vector3 shoulderToTargetDir = (target.position - baseJoint.position).normalized;
        endEffector.position = target.position;  // Move the end effector to the target

        // Step 2: Use the cosine law to calculate the angle for the elbow joint (middleJoint)
        float cosTheta = Mathf.Clamp((Mathf.Pow(upperArmLength, 2) + Mathf.Pow(lowerArmLength, 2) - Mathf.Pow(targetDistance, 2)) / (2 * upperArmLength * lowerArmLength), -1f, 1f);
        float theta = Mathf.Acos(cosTheta);  // Angle at the elbow joint in radians

        // Calculate the angle between the upper arm and the target
        float cosAlpha = Mathf.Clamp((Mathf.Pow(upperArmLength, 2) + Mathf.Pow(targetDistance, 2) - Mathf.Pow(lowerArmLength, 2)) / (2 * upperArmLength * targetDistance), -1f, 1f);
        float alpha = Mathf.Acos(cosAlpha);  // Angle in radians between upper arm and target

        // Step 3: Adjust the middle joint's rotation to point the elbow towards the target while respecting its forward direction
        Vector3 elbowToHandDir = (endEffector.position - middleJoint.position).normalized;  // Direction from elbow to hand
        middleJoint.rotation = Quaternion.LookRotation(elbowToHandDir, middleJoint.up);  // Point the elbow towards the hand while maintaining forward

        // Apply the elbow angle based on the elbow's forward axis
        middleJoint.localRotation *= Quaternion.Euler(theta * Mathf.Rad2Deg, 0, 0);  // Adjust based on the elbow's forward axis

        // Step 4: Adjust the base joint's rotation to point towards the target
        baseJoint.rotation = Quaternion.LookRotation(shoulderToTargetDir, baseJoint.up);  // Base joint points towards the target
        
//         // Step 1: Calculate the direction from base joint to target
// Vector3 baseToTargetDir = (target.position - baseJoint.position).normalized;

// // Step 2: Calculate the base joint's current forward direction
// Vector3 baseForwardDir = baseJoint.forward;  // Assuming the base's local forward is along its forward axis

// // Step 3: Calculate the angle between base joint's forward direction and the target direction
// float baseToTargetAngle = Vector3.Angle(baseForwardDir, baseToTargetDir);  // This gives the angle between the forward vector and the target

// // Step 4: Calculate the base's final rotation angle by subtracting alpha
// float baseRotation = baseToTargetAngle - alpha;  // This is the angle the base joint should rotate
// baseJoint.localRotation *= Quaternion.Euler(baseRotation * Mathf.Rad2Deg, 0, 0);  // Adjust based on the elbow's forward axis


        // Debugging: Draw lines to visualize the skeleton and the target reach
        Debug.DrawLine(baseJoint.position, middleJoint.position, Color.red);  // Shoulder to elbow
        Debug.DrawLine(middleJoint.position, endEffector.position, Color.blue);  // Elbow to hand
        Debug.DrawLine(endEffector.position, target.position, Color.green);  // Hand to target
    }
}





