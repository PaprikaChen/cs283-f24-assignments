using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 4; // 最大生命值
    public int currentHealth; // 当前生命值
    public Image[] hearts; // 心形图标数组
    public Sprite fullHeart; // 满心图标
    public Sprite emptyHeart; // 空心图标

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"Health initialized: {currentHealth}/{maxHealth}"); // 初始化时输出当前生命值
        UpdateHearts();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"TakeDamage called: {damage} damage"); // 调用时输出伤害值
        currentHealth -= damage;
        Debug.Log($"Current health after damage: {currentHealth}"); // 输出掉血后的当前生命值

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die(); // 生命值为 0 时调用死亡逻辑
        }
        UpdateHearts();
    }

    public void Heal(int healAmount)
    {
        Debug.Log($"Heal called: {healAmount} heal"); // 调用时输出治疗量
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log($"Current health after heal: {currentHealth}"); // 输出治疗后的当前生命值
        UpdateHearts();
    }

    void UpdateHearts()
    {
        Debug.Log($"Updating hearts UI. Current health: {currentHealth}"); // 更新心形 UI 时输出当前生命值
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    void Die()
    {
        Debug.Log("Player is dead!"); // 输出玩家死亡日志
        Application.Quit(); // 退出游戏
    }
}
