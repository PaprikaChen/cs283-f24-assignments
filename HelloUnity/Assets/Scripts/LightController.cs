using UnityEngine;

public class LightController : MonoBehaviour
{
    private Light myLight; // Reference to the Light component
    private BehaviorMinion behaviorMinion; // Reference to the BehaviorMinion script

    void Start()
    {
        // Get the Light component on this GameObject
        myLight = GetComponent<Light>();

        // Get the BehaviorMinion script attached to this GameObject
        behaviorMinion = GetComponent<BehaviorMinion>();

        // Check if both components are found
        if (myLight == null)
        {
            Debug.LogError("No Light component found on this GameObject.");
        }
        if (behaviorMinion == null)
        {
            Debug.LogError("No BehaviorMinion script found on this GameObject.");
        }
    }

    void Update()
    {
        // If the BehaviorMinion script exists, check the isNight variable
        if (behaviorMinion != null && myLight != null)
        {
            myLight.enabled = behaviorMinion.isNight; // Enable or disable light based on isNight
        }
    }
}
