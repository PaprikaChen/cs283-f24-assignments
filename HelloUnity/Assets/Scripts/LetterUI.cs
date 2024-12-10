using UnityEngine;
using TMPro;

public class LetterUI : MonoBehaviour
{
    public GameObject[] letters; // 信件UI对象的数组
    public int currentLetterIndex = 0; // 当前显示的信件索引

    private bool isLetterOpen = false; // 当前信件是否打开

    public TextMeshProUGUI letterText;

    void Start()
    {
        // 检查信件数组是否初始化
        if (letters == null || letters.Length == 0)
        {
            Debug.LogError("letters 数组未初始化或为空！");
            return;
        }

        // 遍历数组，确保所有信件对象都不可见
        foreach (GameObject letterUI in letters)
        {
            if (letterUI == null)
            {
                Debug.LogError("letters 数组中存在未赋值的元素！");
            }
            else
            {
                letterUI.SetActive(false);
                Debug.Log($"信件 {letterUI.name} 已设置为不可见");
            }
        }
    }

    // 切换信件的显示状态
    public void ToggleLetter()
    {
        if (letters == null || letters.Length == 0)
        {
            Debug.LogWarning("ToggleLetter 被调用，但 letters 数组为空！");
            return;
        }

        if (letters[currentLetterIndex] == null)
        {
            Debug.LogWarning($"当前索引 {currentLetterIndex} 对应的信件为空！");
            return;
        }

        if (isLetterOpen)
        {
            // 关闭当前信件
            letters[currentLetterIndex].SetActive(false);
            isLetterOpen = false;
            Debug.Log($"信件 {letters[currentLetterIndex].name} 已关闭");
        }
        else
        {
            // 打开当前信件
            letters[currentLetterIndex].SetActive(true);
            isLetterOpen = true;
            Debug.Log($"信件 {letters[currentLetterIndex].name} 已打开");
        }
    }

    // 更新信件索引
    public void UpdateLetterIndex(int newIndex)
    {
        if (newIndex >= 0 && newIndex < letters.Length)
        {
            currentLetterIndex = newIndex;
            Debug.Log($"当前信件索引更新为 {currentLetterIndex + 1}");
            if (letterText != null)
            {
                letterText.text = $"({currentLetterIndex}/5)";
            }

        }
        else
        {
            Debug.LogWarning($"无效的信件索引：{newIndex}");
        }
    }

    void Update()
    {
        // 按下空格键时，关闭信件
        if (isLetterOpen && Input.GetKeyDown(KeyCode.Space))
        {
            ToggleLetter();
        }
    }
}
