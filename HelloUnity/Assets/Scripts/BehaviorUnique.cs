using System.Collections;
using UnityEngine;
using TMPro;

public class BehaviorUnique : BaseNPCBehavior
{
    public GameObject dialogueBox1;
    public TextMeshProUGUI dialogueBox2;
    public float jumpHeight = 2f;
    public float rotationSpeed = 360f;

    protected override void Start()
    {
        base.Start();

        var wander = new AIWander(this);
        var interact = new AIInteract(this, jumpHeight, rotationSpeed, dialogueBox1, dialogueBox2);
        var lookAtPlayer = new AILookAtPlayer(this);

        wander.transitions.Add(new AIState.Transition { fn = () => Vector3.Distance(transform.position, player.position) <= interactDistance, next = interact });
        interact.transitions.Add(new AIState.Transition { fn = () => interact.IsFinished, next = lookAtPlayer });
        lookAtPlayer.transitions.Add(new AIState.Transition { fn = () => Vector3.Distance(transform.position, player.position) > interactDistance, next = wander });

        controller = new AIController(wander);

        dialogueBox1.SetActive(true);
    }
}
