using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public int damage = 1; // 火球的伤害值

    private void OnTriggerEnter(Collider other)
    {
        // 检查是否与玩家碰撞
        if (other.CompareTag("Player"))
        {
            // 获取玩家的HealthSystem组件
            HealthSystem healthSystem = other.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.TakeDamage(damage); // 对玩家造成伤害
            }

            // 销毁火球
            Destroy(gameObject);
        }
    }
}
