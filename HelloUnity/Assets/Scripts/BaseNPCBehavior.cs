using UnityEngine;
using UnityEngine.AI;

public abstract class BaseNPCBehavior : MonoBehaviour
{
    public Transform player;      
    public Transform wanderRange;   
    public Transform homeArea;      
    public float interactDistance = 2f;
    public float attackRange = 4f;

    protected NavMeshAgent agent;
    protected AIController controller;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent is missing!");
            enabled = false;
            return;
        }

        if (player == null || wanderRange == null || homeArea == null)
        {
            Debug.LogError("Player, WanderRange, or HomeArea is not assigned!");
            enabled = false;
        }
    }

    protected virtual void Update()
    {
        controller?.Update(Time.deltaTime);
    }
}
