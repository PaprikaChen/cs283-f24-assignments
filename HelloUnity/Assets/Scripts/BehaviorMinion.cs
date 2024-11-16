using UnityEngine;

public class BehaviorMinion : BaseNPCBehavior
{
    public GameObject ghostFirePrefab;
    public Transform firePoint;

    protected override void Start()
    {
        base.Start();

        var wander = new AIWander(this);
        var attack = new AIAttack(this, ghostFirePrefab);
        var follow = new AIFollow(this);
        var retreat = new AIRetreat(this);

        wander.transitions.Add(new AIState.Transition { fn = () => Vector3.Distance(transform.position, player.position) <= attackRange, next = attack });
        attack.transitions.Add(new AIState.Transition { fn = () => Vector3.Distance(transform.position, player.position) > attackRange, next = follow });
        follow.transitions.Add(new AIState.Transition { fn = () => Vector3.Distance(player.position, homeArea.position) <= homeArea.localScale.x / 2, next = retreat });
        follow.transitions.Add(new AIState.Transition { fn = () => Vector3.Distance(transform.position, player.position) <= attackRange, next = attack });
        retreat.transitions.Add(new AIState.Transition { fn = () => Vector3.Distance(transform.position, wanderRange.position) <= 0.5f, next = wander });
        controller = new AIController(wander);
    }
}
