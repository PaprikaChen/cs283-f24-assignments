using UnityEngine;

public class BusButton : MonoBehaviour
{
    public GameObject player; 
    public GameObject targetLocation; 

    public void OnButtonClick()
    {
        gameObject.SetActive(false);

        if (player != null && targetLocation != null)
        {
            CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = false;
                player.transform.position = targetLocation.transform.position;
                player.transform.rotation = Quaternion.Euler(0, 180, 0);
                controller.enabled = true;

                Debug.Log($"Player moved to {targetLocation.name} at {targetLocation.transform.position}");
            }
            else
            {
                Debug.LogWarning("Player does not have a CharacterController component!");
            }
        }
        else
        {
            if (player == null)
                Debug.LogWarning("Player is not assigned!");
            if (targetLocation == null)
                Debug.LogWarning("Target Location is not assigned!");
        }
    }
}
