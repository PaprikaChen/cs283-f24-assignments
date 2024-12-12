using UnityEngine;
using TMPro;

public class LetterUI : MonoBehaviour
{
    public GameObject[] letters; 
    public int currentLetterIndex = 0; 

    private bool isLetterOpen = false; 

    public TextMeshProUGUI letterText;

    void Start()
    {
        if (letters == null || letters.Length == 0)
        {
            Debug.LogError("letters are not initialized!");
            return;
        }

        foreach (GameObject letterUI in letters)
        {
            if (letterUI == null)
            {
                Debug.LogError("letters are not initialized!");
            }
            else
            {
                letterUI.SetActive(false);
                Debug.Log($"Letter {letterUI.name} is now invisible");
            }
        }
    }

    public void ToggleLetter()
    {
        if (letters == null || letters.Length == 0)
        {
            Debug.LogWarning("ToggleLetter is empty");
            return;
        }

        if (letters[currentLetterIndex] == null)
        {
            Debug.LogWarning($"Letter {currentLetterIndex} is empty");
            return;
        }

        if (isLetterOpen)
        {
            letters[currentLetterIndex].SetActive(false);
            isLetterOpen = false;
            Debug.Log($"Letter{letters[currentLetterIndex].name} is closed");
        }
        else
        {
            letters[currentLetterIndex].SetActive(true);
            isLetterOpen = true;
            Debug.Log($"Letter{letters[currentLetterIndex].name} is open");
        }
    }

    public void UpdateLetterIndex(int newIndex)
    {
        if (newIndex >= 0 && newIndex < letters.Length)
        {
            currentLetterIndex = newIndex;
            Debug.Log($"The current letter index is {currentLetterIndex}");
            if (letterText != null)
            {
                letterText.text = $"({currentLetterIndex}/{letters.Length - 1})";
            }
        }
        else
        {
            Debug.LogWarning($"not valid index{newIndex}");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleLetter();
        }
    }
}
