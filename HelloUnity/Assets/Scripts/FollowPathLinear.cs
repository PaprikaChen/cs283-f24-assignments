using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathLinear : MonoBehaviour
{
    public Transform[] poi;  // Array of Points of Interest
    public float duration = 3.0F;
    private int curPOI = 0;  // Current Point of Interest index
    private Coroutine currentCoroutine;
    private bool isRunning = false;

    void Start()
    {
        if (poi.Length < 2) return;  // Ensure there are enough points
        curPOI = 0;
        transform.position = poi[0].position;  
        transform.rotation = poi[0].rotation;  
        currentCoroutine = StartCoroutine(DoLerp());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isRunning)
            {
                StopCoroutine(currentCoroutine);  
            }

            curPOI = 0;  // Reset to the first point
            transform.position = poi[0].position;  
            transform.rotation = poi[0].rotation;
            currentCoroutine = StartCoroutine(DoLerp());
        }
    }

    IEnumerator DoLerp()
    {
        isRunning = true;

        while (curPOI < poi.Length - 1)
        {
            Vector3 currentPos = poi[curPOI].position;
            Vector3 nextPos = poi[curPOI + 1].position;

            for (float timer = 0; timer < duration; timer += Time.deltaTime)
            {
                float u = timer / duration;
                transform.position = Vector3.Lerp(currentPos, nextPos, u);

                // Calculate direction and update rotation to face next position
                Vector3 directionToNext = nextPos - transform.position;
                if (directionToNext != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(directionToNext);
                    transform.rotation = targetRotation;
                }

                yield return null;
            }

            curPOI++;

            if (curPOI >= poi.Length - 1)
            {
                curPOI = 0;
                isRunning = false;
                yield break;
            }
        }

        isRunning = false;
    }
}


