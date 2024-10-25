using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
public class Gaze : MonoBehaviour
{
	public Transform target;  // The target that the character should look at
	public Transform lookJoint;  // The joint
								 // Start is called before the first frame update
	void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
		//I = Quaternion.identity;
		lookJoint.rotation = Quaternion.identity;
		if (target == null || lookJoint == null) return;
		Vector3 r = lookJoint.forward;
		Vector3 e = target.position - (lookJoint.position + lookJoint.forward);
		Vector3 cross = Vector3.Cross(r, e);
		float normCross = cross.magnitude;
		float dot = Vector3.Dot(r, e);
		// Calculate the rotation angle (ϕ)
		float angle = Mathf.Atan2(normCross, Vector3.Dot(r, r) + dot) * Mathf.Rad2Deg;
		if (angle < 0.1f) return;
		Vector3 rotationAxis = cross.normalized;
		Quaternion rotation = Quaternion.AngleAxis(angle, rotationAxis);
		// because I use a prefab character which already did the "parent" step, I will apply the rotation to the lookjoint itself
		Debug.Log($"new rot: {rotation}");
		Debug.Log($"old rot: {lookJoint.rotation}");
		lookJoint.rotation = rotation * lookJoint.rotation;
		//lookJoint.rotation = rotation * lookJoint.rotation;
		Debug.DrawLine(lookJoint.position, target.position, Color.green);
	}
}