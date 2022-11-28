using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkBehavior : IStateBehavior
{
    private NavMeshAgent agent;
    private Animator animator;

    public WalkBehavior(NavMeshAgent agent, Animator animator)
    {
        this.agent = agent;
        this.animator = animator;
    }

    public void Enter(Transform target)
    {
        animator.SetTrigger("Walk");

        if (agent == null || target == null)
            throw new System.InvalidOperationException($"WalkBehavior agent is null or targetAgent is null!");

        agent.isStopped = false;
        agent.SetDestination(target.position);
    }

    public void Exit()
    {
        agent.isStopped = true;
        animator.SetTrigger("Idle");
    }
}
