using System.Collections;
using UnityEngine;
using BTAI;

public class BehaviorMinion : MonoBehaviour
{
    public Transform player; 
    public Transform homeArea; // home
    public GameObject ghostFirePrefab; 
    public Transform firePoint; 
    public float attackRange = 4f; 
    public Transform wanderRange; 
    public bool isNight = false;
    private UnityEngine.AI.NavMeshAgent agent; // NavMeshAgent

    private bool isAttacking = false; 
    private bool isFollowing = false; 
    private bool isWandering = false;
    private bool isRetreating = false;
    private Root ai; 


    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component is missing from the GameObject.");
            return;
        }

        if (player == null || wanderRange == null || homeArea == null)
        {
            Debug.LogError("One or more required references are missing. Please assign player, wanderRange, and homeArea in the Inspector.");
            return;
        }
ai = BT.Root().OpenBranch(
    BT.Selector().OpenBranch(
        BT.Sequence().OpenBranch(
            BT.Condition(() => player != null 
                              && Vector3.Distance(transform.position, player.position) <= attackRange 
                              && !isAttacking
                              && isNight),
            BT.Call(() => Attack())
        ),
        BT.Sequence().OpenBranch(
            BT.Condition(() => isNight 
                              && isFollowing 
                              && Vector3.Distance(player.position, homeArea.position) > homeArea.localScale.x / 2),
            BT.Call(() => FollowPlayer())
        ),
        BT.Sequence().OpenBranch(
            BT.Condition(() => isFollowing 
                              && (!isNight 
                                  || Vector3.Distance(player.position, homeArea.position) <= homeArea.localScale.x / 2)),
            BT.Call(() => StopFollowing())
        ),
        BT.Sequence().OpenBranch(
            BT.Condition(() => !isAttacking && !isFollowing && !isRetreating),
            BT.Call(() => Wander())
        )
    )
) as Root;



    }

    void Update()
    {
        ai.Tick(); 
    }

    private void Attack()
    {
        if (isAttacking) return; 
        isAttacking = true; 
        isWandering = false;
        StartCoroutine(AttackRoutine());
    }
    private IEnumerator AttackRoutine()
    {
        Debug.Log("Attacking the player!");
        yield return new WaitForSeconds(0.5f); // 等待攻击前的准备时间

        // 确保有火球Prefab和发射点
        if (ghostFirePrefab != null && firePoint != null)
        {
            // 实例化火球Prefab
            GameObject ghostFire = Instantiate(ghostFirePrefab, firePoint.position, Quaternion.identity);

            Vector3 direction = (player.position - firePoint.position);
            direction.y = 0; // 忽略Y轴的高度差
            direction = direction.normalized; // 重新归一化


            // 使火球朝向玩家的方向（忽略Y轴差异，让火球保持水平移动）
            ghostFire.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            // 添加刚体速度，让火球飞向玩家
            Rigidbody rb = ghostFire.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = direction * 10f; // 设置火球速度
            }

            // 3秒后销毁火球
            Destroy(ghostFire, 3f);
        }

        // 等待攻击冷却时间
        yield return new WaitForSeconds(2f); 
        isAttacking = false; // 攻击结束
        isFollowing = true;  // 恢复跟随玩家
    }


    private void FollowPlayer()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            Vector3 targetPosition = player.position - directionToPlayer * 1.5f;

            agent.SetDestination(targetPosition);
        }
    }

    private void StopFollowing()
    {
        if (isRetreating) return;
        Debug.Log("Player entered home area. Returning to wander.");
        isFollowing = false; 
        isRetreating = true;
        isWandering = false;
        agent.SetDestination(wanderRange.position);
        StartCoroutine(WaitUntilArriveAtOriginalPosition());
    }

    private IEnumerator WaitUntilArriveAtOriginalPosition()
    {
        while (Vector3.Distance(transform.position, wanderRange.position) > 0.5f)
        {
            yield return null; 
        }
        Debug.Log("Returned to original position. Switching to Wander state.");
        isRetreating = false;
    }

    private void Wander()
    {
        if (isWandering) return;
        isWandering = true;
        float timer = 0f;
        const float maxTime = 5f;

        SetRandomDestination();
        timer = 0f;

        StartCoroutine(WanderRoutine(timer, maxTime));
    }

    private IEnumerator WanderRoutine(float timer, float maxTime)
    {
        while (!isAttacking && !isFollowing && !isRetreating)
        {
            timer += Time.deltaTime;

            if (!agent.pathPending && agent.remainingDistance < 0.5f || timer >= maxTime)
            {
                SetRandomDestination();
                timer = 0f;
            }
            yield return null;
        }
    }

    private void SetRandomDestination()
    {
        float radius = wanderRange.localScale.x / 2;
        Vector3 randomOffset = new Vector3(
            UnityEngine.Random.Range(-radius, radius),
            0,
            UnityEngine.Random.Range(-radius, radius)
        );
        Vector3 targetPosition = wanderRange.position + randomOffset;

        if (UnityEngine.AI.NavMesh.SamplePosition(targetPosition, out UnityEngine.AI.NavMeshHit hit, radius, UnityEngine.AI.NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}

