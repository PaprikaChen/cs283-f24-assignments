using UnityEngine;
using System.Collections.Generic;

public class DayNightCycle : MonoBehaviour
{
    public Light sun; 
    public Light nightLight;
    public Transform target; 
    public float dayDuration = 60f; 
    public Vector3[] keyPoints; 

    public List<BehaviorMinion> minions; 

    private float currentTime = 0f; 
    private bool isCurrentlyNight = false; 
    void Start()
    {
        if (keyPoints == null || keyPoints.Length != 4)
        {
            Debug.LogError("Please provide exactly 4 key points for the sun's movement.");
            return;
        }

        if (sun == null)
        {
            Debug.LogError("Please assign the sun (Directional Light).");
        }

        minions = new List<BehaviorMinion>(FindObjectsOfType<BehaviorMinion>());
    }

    void Update()
    {
        if (sun == null || target == null || keyPoints == null || keyPoints.Length != 4) return;

        currentTime += Time.deltaTime / dayDuration;
        if (currentTime >= 1f)
        {
            currentTime = 0f; 
        }

        Vector3 newPosition = InterpolatePosition(currentTime);
        sun.transform.position = newPosition;

        sun.transform.LookAt(target);
        UpdateSunProperties();
        HandleAmbientLight();

    }

    Vector3 InterpolatePosition(float time)
    {

        if (time < 0.25f)
        {
            float t = time / 0.25f;
            return Vector3.Lerp(keyPoints[0], keyPoints[1], t);
        }
        else if (time < 0.5f)
        {
            float t = (time - 0.25f) / 0.25f;
            return Vector3.Lerp(keyPoints[1], keyPoints[2], t);
        }
        else if (time < 0.75f)
        {
            float t = (time - 0.5f) / 0.25f;
            return Vector3.Lerp(keyPoints[2], keyPoints[3], t);
        }
        else
        {
            float t = (time - 0.75f) / 0.25f;
            return Vector3.Lerp(keyPoints[3], keyPoints[0], t);
        }


    }
    void UpdateSunProperties()
    {
        float normalizedTime = currentTime;

        Color sunriseColor = new Color(0.9f, 0.7f, 0.2f); 
        Color middayColor = Color.white;
        Color sunsetColor = new Color(0.9f, 0.7f, 0.2f); 

        if (normalizedTime <= 0.25f)
        {
            float t = normalizedTime / 0.25f;
            sun.color = Color.Lerp(sunriseColor, middayColor, t);
            sun.intensity = Mathf.Lerp(0.2f, 1f, t);
            nightLight.intensity = Mathf.Lerp(0.8f, 0f, t); 
        }
        else if (normalizedTime <= 0.5f) 
        {
            float t = (normalizedTime - 0.25f) / 0.25f;
            sun.color = middayColor;
            sun.intensity = Mathf.Lerp(1f, 0.2f, t);
            sun.color = Color.Lerp(middayColor, sunsetColor, t);
            nightLight.intensity = 0f; 
        }
        else if (normalizedTime <= 0.75f) 
        {
            float t = (normalizedTime - 0.5f) / 0.25f;
            nightLight.intensity = Mathf.Lerp(0f, 0.8f, t); 
        }

    }

void HandleAmbientLight()
    {
        Color nightAmbientColor = new Color(0.05f, 0.1f, 0.8f); 
        Color dayAmbientColor = new Color(0.9f, 0.9f, 0.8f);  

        if (currentTime <= 0.5f)
        {
            float t = currentTime / 0.5f;
            RenderSettings.ambientLight = Color.Lerp(nightAmbientColor, dayAmbientColor, t);
        }
        else
        {
            float t = (currentTime - 0.5f) / 0.5f;
            RenderSettings.ambientLight = Color.Lerp(dayAmbientColor, nightAmbientColor, t);
        }

        bool isNightNow = currentTime > 0.5f;
        if (isNightNow != isCurrentlyNight)
        {
            isCurrentlyNight = isNightNow;
            foreach (var minion in minions)
            {
                minion.isNight = isNightNow;
            }
        }
    }



}
