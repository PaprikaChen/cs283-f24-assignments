using UnityEngine;

public class GameQuitHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }
    public void QuitGame()
    {
        Debug.Log("Exiting game..."); 
        Application.Quit();
    }
}
