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

    // private void UpdateUI()
    // {
    //     if (collectionCount < 20)
    //     {
    //         collectionCountText.text = "Collect 20 Mushrooms! You collected " + collectionCount;
    //     }
    //     else
    //     {
    //         collectionCountText.text = "You won!";
    //         StartCoroutine(WaitAndQuit());
    //     }
    // }

    private IEnumerator WaitAndQuit()
    {
        yield return new WaitForSeconds(3); 
        Application.Quit();
    }

}