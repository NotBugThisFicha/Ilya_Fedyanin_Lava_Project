using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimBehavior : IStateBehavior
{
    private Animator animator;
    private string parametrAnimName;

    public AnimBehavior(Animator animator, string parametr)
    {
        this.animator = animator;
        parametrAnimName = parametr;
    }

    public void Enter(Transform target)
    {
        animator.SetTrigger(parametrAnimName);
    }

    public void Exit()
    {
        animator.SetTrigger("Idle");
    }
}
