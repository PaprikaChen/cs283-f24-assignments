using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float linearSpeed = 5f;
    public float turningSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            transform.Translate(Vector3.forward * linearSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.S)) {
            transform.Translate(Vector3.back * linearSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.up, -turningSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.up, turningSpeed * Time.deltaTime);
        }
    }
}
