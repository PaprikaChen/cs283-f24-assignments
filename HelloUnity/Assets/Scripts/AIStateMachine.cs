using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public abstract class AIState
{
    public class Transition
    {
        public Func<bool> fn;
        public AIState next;
    }

    public List<Transition> transitions = new List<Transition>();
    protected bool isFinished = false;

    public virtual void Enter() { isFinished = false; }
    public virtual void Exit() { }
    public virtual void Update(float dt)
    {
        foreach (var transition in transitions)
        {
            if (transition.fn())
            {
                isFinished = true;
                break;
            }
        }
    }

    public bool IsTriggered() { return isFinished; }
    public AIState Next()
    {
        foreach (var transition in transitions)
        {
            if (transition.fn())
            {
                return transition.next;
            }
        }
        return null; 
    }

}

public class AIController
{
    private AIState current;

    public AIController(AIState initial)
    {
        current = initial;
        current.Enter();
    }

    public void Update(float dt)
    {
        if (current == null) return;

        current.Update(dt);
        if (current.IsTriggered())
        {
            current.Exit();
            current = current.Next();
            current.Enter();
        }
    }
}

public class AIWander : AIState
{
    private BaseNPCBehavior entity;
    private NavMeshAgent agent;
    private float timer = 0f;
    private const float maxTime = 5f;

    public AIWander(BaseNPCBehavior entity)
    {
        this.entity = entity;
        agent = entity.GetComponent<NavMeshAgent>();
    }

    public override void Enter()
    {
        base.Enter();
        SetRandomDestination();
        timer = 0f;
    }

    public override void Update(float dt)
    {
        base.Update(dt);
        timer += dt;

        if (!agent.pathPending && agent.remainingDistance < 0.5f || timer >= maxTime)
        {
            SetRandomDestination();
            timer = 0f;
        }
    }

    private void SetRandomDestination()
    {
        float radius = entity.wanderRange.localScale.x / 2;
        Vector3 randomOffset = new Vector3(
            UnityEngine.Random.Range(-radius, radius),
            0,
            UnityEngine.Random.Range(-radius, radius)
        );
        Vector3 targetPosition = entity.wanderRange.position + randomOffset;

        if (NavMesh.SamplePosition(targetPosition, out NavMeshHit hit, radius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}


public class AIInteract : AIState
{
    private BaseNPCBehavior entity;
    private Transform player;
    private float jumpHeight;
    private float rotationSpeed;
    private bool hasJumped = false;
    private GameObject dialogue1;
    private TextMeshProUGUI dialogue2;

    public AIInteract(BaseNPCBehavior entity, float jumpHeight, float rotationSpeed, GameObject dialogue1, TextMeshProUGUI dialogue2)
    {
        this.entity = entity;
        this.player = entity.player;
        this.jumpHeight = jumpHeight;
        this.rotationSpeed = rotationSpeed;
        this.dialogue1 = dialogue1;
        this.dialogue2 = dialogue2;
    }

    public override void Enter()
    {
        base.Enter();

        FacePlayer();

        entity.StartCoroutine(JumpAndSpin());
        dialogue1.SetActive(false);
    }

    private void FacePlayer()
    {
        if (player == null) return;

        Vector3 direction = (player.position - entity.transform.position).normalized;

        direction.y = 0;

        entity.transform.rotation = Quaternion.LookRotation(direction);
    }

    private IEnumerator JumpAndSpin()
    {
        if (hasJumped) yield break;
        float originalY = entity.transform.position.y;
        float jumpTime = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < jumpTime)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / jumpTime;

            float yOffset = Mathf.Sin(Mathf.PI * progress) * jumpHeight;
            entity.transform.position = new Vector3(
                entity.transform.position.x,
                originalY + yOffset,
                entity.transform.position.z
            );

            yield return null;
        }

        entity.StartCoroutine(ShowDialogueForDuration());

        yield return new WaitForSeconds(5f);

        entity.transform.position = new Vector3(entity.transform.position.x, originalY, entity.transform.position.z);
        hasJumped = true;
        isFinished = true;
    }

    private IEnumerator ShowDialogueForDuration()
    {
        dialogue1.SetActive(false);
        dialogue2.text = "Try to reach the park at the heart of the maze... But be careful—the creatures in there aren't as friendly as I am...";

        yield return new WaitForSeconds(6f);

        dialogue1.SetActive(false);
        dialogue2.text = "";
    }

    public bool IsFinished => hasJumped;
}

public class AILookAtPlayer : AIState
{
    private BaseNPCBehavior entity;
    private Transform player;

    public AILookAtPlayer(BaseNPCBehavior entity)
    {
        this.entity = entity;
        this.player = entity.player;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update(float dt)
    {
        base.Update(dt);

        if (player != null)
        {
            float distance = Vector3.Distance(player.position, entity.transform.position);

            if (distance < 0.2f)
            {  
                return; 
            }

            Vector3 direction = (player.position - entity.transform.position).normalized;
            direction.y = 0; 
            entity.transform.rotation = Quaternion.LookRotation(direction);
        }
    }

}

public class AIAttack : AIState
{
    private BehaviorMinion entity;
    private Transform player;
    private Transform firePoint;
    private GameObject ghostFirePrefab;
    private float attackCooldown = 2f;
    private float attackTimer = 0f;

    public AIAttack(BehaviorMinion entity, GameObject ghostFirePrefab)
    {
        this.entity = entity;
        this.player = entity.player;
        this.firePoint = entity.firePoint;
        this.ghostFirePrefab = ghostFirePrefab;
    }

    public override void Enter()
    {
        base.Enter();
        attackTimer = 0f;
    }

    public override void Update(float dt)
    {
        base.Update(dt);
        attackTimer += dt;

        if (player != null)
        {
            float distance = Vector3.Distance(player.position, entity.transform.position);

            if (distance < 0.5f)
            {
                return;
            }

            Vector3 direction = (player.position - entity.transform.position).normalized;
            entity.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        }

        if (attackTimer >= attackCooldown)
        {
            FireGhostFire();
            attackTimer = 0f;
        }
    }


    private void FireGhostFire()
    {
        if (ghostFirePrefab != null && firePoint != null)
        {
            GameObject ghostFire = GameObject.Instantiate(ghostFirePrefab, firePoint.position, Quaternion.identity);

            Vector3 direction = (player.position - firePoint.position).normalized;
            direction.y = 0;
            direction.Normalize();

            ghostFire.transform.rotation = Quaternion.LookRotation(direction);

            Rigidbody rb = ghostFire.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = direction * 5f;
            }

            GameObject.Destroy(ghostFire, 2f);
        }
    }
}

public class AIFollow : AIState
{
    private BaseNPCBehavior entity;
    private NavMeshAgent agent;
    private Transform player;

    public AIFollow(BaseNPCBehavior entity)
    {
        this.entity = entity;
        agent = entity.GetComponent<NavMeshAgent>();
        player = entity.player;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update(float dt)
    {
        base.Update(dt);
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }
}

public class AIRetreat : AIState
{
    private BaseNPCBehavior entity;
    private NavMeshAgent agent;

    public AIRetreat(BaseNPCBehavior entity)
    {
        this.entity = entity;
        agent = entity.GetComponent<NavMeshAgent>();
    }

    public override void Enter()
    {
        base.Enter();
        agent.SetDestination(entity.wanderRange.position);
    }

    public override void Update(float dt)
    {
        base.Update(dt);
    }
}
