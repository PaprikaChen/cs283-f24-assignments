using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    public float floatSpeed = 0.3f; 
    public float floatAmplitude = 0.2f; 

    private Vector3 startPosition;

    void Start()
    {
 
        startPosition = transform.position;
    }

    void Update()
    {

        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
