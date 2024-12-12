using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public CanvasGroup canvas1; // First canvas 
    public CanvasGroup canvas2; // Second canvas 

    void Start()
    {
        // Ensure Canvas1 is visible and Canvas2 is hidden initially
        canvas1.alpha = 1f;
        canvas2.alpha = 0f;

        // Start the sequence
        StartCoroutine(PlayIntroSequence());
    }

    private IEnumerator PlayIntroSequence()
    {
        // Wait for 3 seconds with Canvas1 visible
        yield return new WaitForSeconds(3f);

        // Hide Canvas1
        canvas1.alpha = 0f;

        // Show Canvas2 immediately
        canvas2.alpha = 1f;

        // Wait for 5 seconds before starting fade-out
        yield return new WaitForSeconds(7f);

        // Gradually fade out Canvas2
        float fadeDuration = 2f;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvas2.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            yield return null;
        }

        // Ensure Canvas2 is fully hidden
        canvas2.alpha = 0f;
    }
}
