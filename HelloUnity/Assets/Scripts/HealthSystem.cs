using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 4; 
    public int currentHealth; 
    public Image[] hearts; 
    public Sprite fullHeart; 
    public Sprite emptyHeart; 

    public Transform respawnPoint; 
    public TextMeshProUGUI deathMessage; 

    private CharacterController characterController; 

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"Health initialized: {currentHealth}/{maxHealth}"); 
        UpdateHearts();

        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController not found on the player object!");
        }

        if (deathMessage != null)
        {
            deathMessage.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"TakeDamage called: {damage} damage"); 
        currentHealth -= damage;
        Debug.Log($"Current health after damage: {currentHealth}"); 

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            StartCoroutine(Die()); 
        }
        UpdateHearts();
    }

    public void Heal(int healAmount)
    {
        Debug.Log($"Heal called: {healAmount} heal"); 
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log($"Current health after heal: {currentHealth}"); 
        UpdateHearts();
    }

    void UpdateHearts()
    {
        Debug.Log($"Updating hearts UI. Current health: {currentHealth}"); 
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
        Debug.Log("Player is dead! Respawning..."); 

        if (deathMessage != null)
        {
            deathMessage.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(3f);

        if (deathMessage != null)
        {
            deathMessage.gameObject.SetActive(false);
        }

        currentHealth = maxHealth;
        UpdateHearts();

        if (characterController != null && respawnPoint != null)
        {
            characterController.enabled = false; 
            transform.position = respawnPoint.position;
            transform.rotation = Quaternion.Euler(0, 180, 0); 
            characterController.enabled = true; 
        }
        else
        {
            Debug.LogWarning("RespawnPoint is not assigned or CharacterController is missing!");
        }
    }
}
