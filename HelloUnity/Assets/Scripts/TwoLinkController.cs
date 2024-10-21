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
        float upperArmLength = Vector3.Distance(baseJoint.position, middleJoint.position); 
        float lowerArmLength = Vector3.Distance(middleJoint.position, endEffector.position);  
        float targetDistance = Vector3.Distance(baseJoint.position, target.position);  

        targetDistance = Mathf.Min(targetDistance, upperArmLength + lowerArmLength);


        Vector3 shoulderToTargetDir = (target.position - baseJoint.position).normalized;
        endEffector.position = target.position;  

        float cosTheta = Mathf.Clamp((Mathf.Pow(upperArmLength, 2) + Mathf.Pow(lowerArmLength, 2) - Mathf.Pow(targetDistance, 2)) / (2 * upperArmLength * lowerArmLength), -1f, 1f);
        float theta = Mathf.Acos(cosTheta);  


        float cosAlpha = Mathf.Clamp((Mathf.Pow(upperArmLength, 2) + Mathf.Pow(targetDistance, 2) - Mathf.Pow(lowerArmLength, 2)) / (2 * upperArmLength * targetDistance), -1f, 1f);
        float alpha = Mathf.Acos(cosAlpha);  
        Vector3 elbowToHandDir = (endEffector.position - middleJoint.position).normalized;  
        middleJoint.rotation = Quaternion.LookRotation(elbowToHandDir, middleJoint.up);

        middleJoint.localRotation *= Quaternion.Euler(theta * Mathf.Rad2Deg, 0, 0);

        baseJoint.rotation = Quaternion.LookRotation(shoulderToTargetDir, baseJoint.up);  


        Debug.DrawLine(baseJoint.position, middleJoint.position, Color.red);  
        Debug.DrawLine(middleJoint.position, endEffector.position, Color.blue);  
        Debug.DrawLine(endEffector.position, target.position, Color.green);  // Hand to target
    }
}





