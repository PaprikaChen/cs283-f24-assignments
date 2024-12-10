using UnityEngine;

public class sendLetterButton : MonoBehaviour
{
    public GameObject player; 
    public GameObject endingPic;
    public GameObject LetterButton;
    public GameObject MomCharacter;

    void Start()
    {
        if (endingPic != null)
        {
            endingPic.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Ending picture is not assigned in the Inspector!");
        }

        if (endingPic != null)
        {
            MomCharacter.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Mom is not assigned in the Inspector!");
        }

    }

    public void OnButtonClick()
    {
        // 获取 LetterUI 脚本并读取 currentLetterIndex 的值
        LetterUI letterUI = LetterButton.GetComponent<LetterUI>();

        if (letterUI != null)
        {
            // 判断 currentLetterIndex 是否为 5
            if (letterUI.currentLetterIndex == 5)
            {
                if (player != null && endingPic != null && LetterButton != null)
                {
                    // 执行动作
                    gameObject.SetActive(false);
                    endingPic.SetActive(true);
                    MomCharacter.SetActive(true);
                }
                else
                {
                    // 检查是否有未分配的对象
                    if (player == null)
                        Debug.LogWarning("Player is not assigned!");
                    if (endingPic == null)
                        Debug.LogWarning("EndingPic is not assigned!");
                }
            }
            else
            {
                Debug.Log("CurrentLetterIndex is not 5, no action performed.");
            }
        }
        else
        {
            Debug.LogWarning("LetterUI component is not found on LetterButton!");
        }
    }

}
