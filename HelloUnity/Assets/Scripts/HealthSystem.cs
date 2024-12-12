using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 4; // 最大生命值
    public int currentHealth; // 当前生命值
    public Image[] hearts; // 心形图标数组
    public Sprite fullHeart; // 满心图标
    public Sprite emptyHeart; // 空心图标

    public Transform respawnPoint; // 重生点
    public TextMeshProUGUI deathMessage; // 死亡消息文本

    private CharacterController characterController; // 角色控制器

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"Health initialized: {currentHealth}/{maxHealth}"); // 初始化时输出当前生命值
        UpdateHearts();

        // 获取角色控制器
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController not found on the player object!");
        }

        // 确保死亡消息初始状态为隐藏
        if (deathMessage != null)
        {
            deathMessage.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"TakeDamage called: {damage} damage"); // 调用时输出伤害值
        currentHealth -= damage;
        Debug.Log($"Current health after damage: {currentHealth}"); // 输出掉血后的当前生命值

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            StartCoroutine(Die()); // 生命值为 0 时调用死亡逻辑
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

    IEnumerator Die()
    {
        Debug.Log("Player is dead! Respawning..."); // 输出玩家死亡日志

        // 显示死亡消息
        if (deathMessage != null)
        {
            deathMessage.gameObject.SetActive(true);
        }

        // 等待 3 秒
        yield return new WaitForSeconds(3f);

        // 隐藏死亡消息
        if (deathMessage != null)
        {
            deathMessage.gameObject.SetActive(false);
        }

        // 重置生命值
        currentHealth = maxHealth;
        UpdateHearts();

        // 传送玩家到重生点并重置旋转
        if (characterController != null && respawnPoint != null)
        {
            characterController.enabled = false; // 禁用角色控制器以防止位置冲突
            transform.position = respawnPoint.position;
            transform.rotation = Quaternion.Euler(0, 180, 0); // 设置旋转为 (0, 180, 0)
            characterController.enabled = true; // 重新启用角色控制器
        }
        else
        {
            Debug.LogWarning("RespawnPoint is not assigned or CharacterController is missing!");
        }
    }
}
