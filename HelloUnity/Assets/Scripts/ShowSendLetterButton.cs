using UnityEngine;

public class ShowSendLetterButton : MonoBehaviour
{
    public GameObject sendLetterButton; 
    public string playerTag = "Player"; 
    public GameObject endingPic;

    void Start()
    {
        if (sendLetterButton != null)
        {
            sendLetterButton.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Button is not assigned in the Inspector!");
        }

        if (endingPic != null)
        {
            endingPic.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Ending picture is not assigned in the Inspector!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (sendLetterButton != null)
            {
                sendLetterButton.SetActive(true); 
                Debug.Log("Player entered the area, showing the send letter button.");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (sendLetterButton != null)
            {
                sendLetterButton.SetActive(false); 
                Debug.Log("Player left the area, hiding the button.");
            }
        }
    }
}
