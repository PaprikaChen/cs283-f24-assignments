using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathCubic : MonoBehaviour
{
    public Transform[] poi; 
    public float duration = 3.0F;
    public bool useDeCasteljau = true; 
    private int curPOI = 0;
    private Coroutine currentCoroutine;

    void Start()
    {
        if (poi.Length < 3) return;  // Ensure there are enough points
        currentCoroutine = StartCoroutine(FollowBezierPath());
    }

    IEnumerator FollowBezierPath()
    {
        while (curPOI < poi.Length - 1)
        {
            Vector3 b0 = poi[curPOI].position;
            Vector3 b3 = poi[curPOI + 1].position;

            Vector3[] controlPoints = CalculateControlPoints(b0, b3, curPOI);
            Vector3 b1 = controlPoints[0];
            Vector3 b2 = controlPoints[1];

            for (float timer = 0; timer < duration; timer += Time.deltaTime)
            {
                float t = timer / duration;
                Vector3 pos = useDeCasteljau ? DeCasteljau(b0, b1, b2, b3, t) : Bezier(b0, b1, b2, b3, t);

                transform.position = pos;

                float nextT = Mathf.Clamp01(t + 0.01f); 
                Vector3 nextPos = useDeCasteljau ? DeCasteljau(b0, b1, b2, b3, nextT) : Bezier(b0, b1, b2, b3, nextT);
                Vector3 forwardDir = (nextPos - pos).normalized;

                if (forwardDir != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(forwardDir);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
                }

                yield return null;
            }

            curPOI++;
            if (curPOI >= poi.Length - 1) yield break;
        }
    }

    Vector3[] CalculateControlPoints(Vector3 b0, Vector3 b3, int curPOI)
    {
        Vector3 b1, b2;

        if (curPOI == 0) 
        {
            // First segment: b1 = b0 + (1/6) * (b3 - b0)
            b1 = b0 + (1f / 6f) * (b3 - b0);
        }
        else
        {
            //b1 = b0 + (1/6) * (pi - pi-2)
            Vector3 p_current = poi[curPOI + 1].position;
            Vector3 p_prev = poi[curPOI - 1].position;
            b1 = b0 + (1f / 6f) * (p_current - p_prev);
        }

        if (curPOI + 2 >= poi.Length) 
        {
            // Last segment: b2 = b3 - (1/6) * (b3 - b0)
            b2 = b3 - (1f / 6f) * (b3 - b0);
        }
        else
        {
            //b2 = b3 - (1/6) * (pi+1 - pi-1)
            Vector3 p_next = poi[curPOI + 2].position;
            Vector3 p_prev = poi[curPOI].position;
            b2 = b3 - (1f / 6f) * (p_next - p_prev);
        }

        return new Vector3[] { b1, b2 };
    }

    // Compute using De Casteljau's algorithm
    Vector3 DeCasteljau(Vector3 b0, Vector3 b1, Vector3 b2, Vector3 b3, float t)
    {
        Vector3 p0 = Vector3.Lerp(b0, b1, t);
        Vector3 p1 = Vector3.Lerp(b1, b2, t);
        Vector3 p2 = Vector3.Lerp(b2, b3, t);
        Vector3 pFinal0 = Vector3.Lerp(p0, p1, t);
        Vector3 pFinal1 = Vector3.Lerp(p1, p2, t);
        return Vector3.Lerp(pFinal0, pFinal1, t);
    }

    // Compute using the Bezier polynomial formula
    Vector3 Bezier(Vector3 b0, Vector3 b1, Vector3 b2, Vector3 b3, float t)
    {
        return Mathf.Pow(1 - t, 3) * b0 + 
               3 * Mathf.Pow(1 - t, 2) * t * b1 + 
               3 * (1 - t) * Mathf.Pow(t, 2) * b2 + 
               Mathf.Pow(t, 3) * b3;
    }

    void OnDrawGizmos()
    {
        if (poi.Length < 3) return;

        Gizmos.color = Color.blue;
        for (int i = 0; i < poi.Length - 1; i++)
        {
            Vector3 b0 = poi[i].position;
            Vector3 b3 = poi[i + 1].position;

            // Call CalculateControlPoints instead of using inline calculation
            Vector3[] controlPoints = CalculateControlPoints(b0, b3, i);
            Vector3 b1 = controlPoints[0];
            Vector3 b2 = controlPoints[1];

            // Draw the Bezier curve as a Gizmo
            for (float t = 0; t < 1; t += 0.05f)
            {
                Vector3 pos = Bezier(b0, b1, b2, b3, t);
                Gizmos.DrawSphere(pos, 0.1f);
            }
        }
    }
}





