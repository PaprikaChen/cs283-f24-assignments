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

        // 日出和日落颜色
        Color sunriseColor = new Color(0.9f, 0.7f, 0.2f); // 日出橙色
        Color middayColor = Color.white; // 白天白色
        Color sunsetColor = new Color(0.9f, 0.7f, 0.2f); // 日落橙色

        if (normalizedTime <= 0.25f) // 日出
        {
            float t = normalizedTime / 0.25f;
            sun.color = Color.Lerp(sunriseColor, middayColor, t);
            sun.intensity = Mathf.Lerp(0.4f, 1f, t);
            nightLight.intensity = Mathf.Lerp(0.8f, 0f, t); // 夜晚光逐渐减弱
        }
        else if (normalizedTime <= 0.5f) // 白天
        {
            float t = (normalizedTime - 0.25f) / 0.25f;
            sun.color = middayColor;
            sun.intensity = 1f;
            nightLight.intensity = 0f; // 白天光为 0
        }
        else if (normalizedTime <= 0.75f) // 日落
        {
            float t = (normalizedTime - 0.5f) / 0.25f;
            sun.color = Color.Lerp(middayColor, sunsetColor, t);
            sun.intensity = Mathf.Lerp(1f, 0.4f, t);
            nightLight.intensity = Mathf.Lerp(0f, 0.8f, t); // 夜晚光逐渐增强
        }

    }

void HandleAmbientLight()
    {
        // 动态调整环境光
        Color nightAmbientColor = new Color(0.05f, 0.1f, 0.8f); // 夜晚深蓝
        Color dayAmbientColor = new Color(0.9f, 0.9f, 0.8f);   // 白天柔和白色

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

        // 更新怪物是否活跃
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
