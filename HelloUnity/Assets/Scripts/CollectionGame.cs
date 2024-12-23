using UnityEngine;
using TMPro;
using System.Collections;

public class CollectionGame : MonoBehaviour
{
     private int collectionCount = 0; 

    private void Start()
    {
        //UpdateUI(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            collectionCount++;
            //UpdateUI(); 

            DisappearEffect collectable = other.GetComponent<DisappearEffect>();
            if (collectable != null) {
                collectable.RunPickupAnimation();
            }
        }
    }

    private IEnumerator WaitAndQuit()
    {
        yield return new WaitForSeconds(3); 
        Application.Quit();
    }

}