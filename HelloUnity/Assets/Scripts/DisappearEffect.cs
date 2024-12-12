using System.Collections;
using UnityEngine;

public class DisappearEffect : MonoBehaviour
{
    public float animationDuration = 2f;
    public Vector3 targetScale = new Vector3(0.2f, 0.2f, 0.2f);
    public float rotationAngle = 180f;
    public float height = 3f;
    public float rotationSpeed = 50f; 
    public int healAmount = 1; 

    private AudioSource audioSource;
    private bool isBeingCollected = false; 

    public void RunPickupAnimation()
    {
        if (audioSource != null)
        {
            audioSource.Play(); 
        }
        isBeingCollected = true; 
        StartCoroutine(PickupAnimationCoroutine());
    }

    private IEnumerator PickupAnimationCoroutine()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, height, 0);
        Vector3 startScale = transform.localScale;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(startRotation.eulerAngles + new Vector3(0, rotationAngle, 0));

        float elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            float progress = elapsedTime / animationDuration;
            transform.position = Vector3.Lerp(startPosition, endPosition, progress);
            transform.localScale = Vector3.Lerp(startScale, targetScale, progress);
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, progress);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;
        transform.localScale = targetScale;
        transform.rotation = endRotation;

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthSystem healthSystem = other.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                Debug.Log($"Player healed by {healAmount}. Current health: {healthSystem.currentHealth}");
                healthSystem.Heal(healAmount); 
            }

            RunPickupAnimation(); 
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isBeingCollected)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }
    }
}
