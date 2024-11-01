using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearEffect : MonoBehaviour
{
    public float animationDuration = 2f;
    public Vector3 targetScale = new Vector3(0.2f, 0.2f, 0.2f);
    public float rotationAngle = 180;
    public float height = 3f;

    private AudioSource audioSource;
    // Start is called before the first frame update

    public void RunPickupAnimation() {
        if (audioSource != null)
        {
            audioSource.Play(); // Play the sound effect
        }
        StartCoroutine(PickupAnimationCoroutine());
    }

    private IEnumerator PickupAnimationCoroutine() {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, height, 0);
        Vector3 startScale = transform.localScale;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(startRotation.eulerAngles + new Vector3(0, rotationAngle, 0));

        float elapsedTime = 0f;
        while (elapsedTime < animationDuration) {
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

        // hide the object
        gameObject.SetActive(false);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
