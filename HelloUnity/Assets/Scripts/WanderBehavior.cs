using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using BTAI;

public class WanderBehavior : MonoBehaviour
{
    public Transform wanderRange;  
    private Root m_btRoot = BT.Root();

    void Start()
    {
        BTNode moveTo = BT.RunCoroutine(MoveToRandom);

        Sequence sequence = BT.Sequence();
        sequence.OpenBranch(moveTo);

        m_btRoot.OpenBranch(sequence);
    }

    void Update()
    {
        m_btRoot.Tick();
    }

    IEnumerator<BTState> MoveToRandom()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        Vector3 target = GenerateRandomPointWithinRange();
        Vector3 targetPosition = new Vector3(target.x, transform.position.y, target.z);
        agent.SetDestination(targetPosition);

        float timer = 0f; 
        float maxTime = 5f; 

        while ((agent.pathPending || agent.remainingDistance > 0.5f) && timer < maxTime)
        {
            timer += Time.deltaTime; 
            yield return BTState.Continue;
        }

        yield return BTState.Success; 
    }

    Vector3 GenerateRandomPointWithinRange()
    {
        if (wanderRange == null)
        {
            Debug.LogError("Wander range is not assigned!");
            return transform.position;
        }

        float radius = wanderRange.localScale.x / 2;
        Vector3 randomOffset = new Vector3(
            Random.Range(-radius, radius),
            0,
            Random.Range(-radius, radius)
        );

        return wanderRange.position + randomOffset; 
    }
}
