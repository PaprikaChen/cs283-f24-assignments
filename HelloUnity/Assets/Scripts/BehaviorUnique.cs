using System;
using System.Collections;
using UnityEngine;
using TMPro;
using BTAI;

public class BehaviorUnique : MonoBehaviour
{
    public Transform player;  
    public float interactDistance = 2f;
    public GameObject dialogueBox1;
    public TextMeshProUGUI dialogueBox2;
    public GameObject background;

    public float jumpHeight = 2f;
    public float rotationSpeed = 360f;
    private UnityEngine.AI.NavMeshAgent agent;
    public Transform wanderRange;
    public GameObject lettericon;

    private bool interactionComplete = false;
    private bool isInteracting = false; 
    private bool isWandering = false;
    private Root ai;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component is missing from the GameObject.");
            return;
        }
        ai = BT.Root().OpenBranch(
            BT.Selector().OpenBranch(
                BT.Sequence().OpenBranch(
                    BT.Condition(() => player != null 
                                    && Vector3.Distance(transform.position, player.position) <= interactDistance 
                                    && !isInteracting), 
                    BT.Call(() => Interact())
                ),
                BT.Sequence().OpenBranch(
                    BT.Condition(() => InteractFinished()),
                    BT.Call(() => LookAtPlayer())
                ),
                BT.Sequence().OpenBranch(
                    BT.Condition(() => !isInteracting),
                    BT.Call(() => Wander())
                )
            )
        ) as Root; 
        dialogueBox1.SetActive(true); 
        dialogueBox2.gameObject.SetActive(false);
        if (background != null) {
            background.SetActive(false);
        }

        if (lettericon != null) {
            lettericon.SetActive(false);
        }
    }


    void Update()
    {
        ai.Tick(); 
    }

    private bool InteractFinished()
    {
        return interactionComplete;
    }

    private void Interact()
    {
        if (isInteracting || interactionComplete) return;
        isInteracting = true;
        isWandering = false;

        StartCoroutine(JumpAndShowDialogue());
        Debug.Log("Interacting with the player.");
    }

    private IEnumerator JumpAndShowDialogue()
    {
        if (lettericon != null) {
            lettericon.SetActive(true);
        }

        float originalY = transform.position.y;
        float jumpTime = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < jumpTime)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / jumpTime;

            float yOffset = Mathf.Sin(Mathf.PI * progress) * jumpHeight;
            transform.position = new Vector3(transform.position.x, originalY + yOffset, transform.position.z);

            yield return null;
        }

        dialogueBox1.SetActive(false);
        dialogueBox2.gameObject.SetActive(true);
        if (background != null) {
            background.SetActive(true);
        }

        yield return new WaitForSeconds(6f);

        dialogueBox2.text = "";
        if (background != null) {
            background.SetActive(false);
        }

        interactionComplete = true;
        isInteracting = false;
    }

    private void LookAtPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0; 
            transform.rotation = Quaternion.LookRotation(direction);
            Debug.Log("Looking at the player.");
        }
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
        while (!isInteracting)
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
