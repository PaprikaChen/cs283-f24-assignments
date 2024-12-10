using UnityEngine;

public class ShowBusTransportButton : MonoBehaviour
{
    public GameObject busTransportButton; 
    public string playerTag = "Player"; 

    void Start()
    {
        if (busTransportButton != null)
        {
            busTransportButton.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Bus Transport Button is not assigned in the Inspector!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (busTransportButton != null)
            {
                busTransportButton.SetActive(true); 
                Debug.Log("Player entered the area, showing the bus transport button.");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (busTransportButton != null)
            {
                busTransportButton.SetActive(false); 
                Debug.Log("Player left the area, hiding the bus transport button.");
            }
        }
    }
}
