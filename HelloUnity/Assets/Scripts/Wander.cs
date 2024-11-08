using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    public float wanderRadius = 10f; 
    public float wanderInterval = 3f; 

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderInterval; 
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderInterval || agent.remainingDistance < 0.5f)
        {
            Vector3 newPos = GetRandomNavMeshPosition();
            agent.SetDestination(newPos);
            timer = 0f; 
        }
    }

    Vector3 GetRandomNavMeshPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius; 
        randomDirection += transform.position; 

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, NavMesh.AllAreas))
        {
            return hit.position; 
        }

        return transform.position; 
    }
}
