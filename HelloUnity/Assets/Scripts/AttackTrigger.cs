using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public int damage = 1; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthSystem healthSystem = other.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.TakeDamage(damage); 
            }

            Destroy(gameObject);
        }
    }
}
