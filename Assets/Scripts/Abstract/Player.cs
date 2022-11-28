using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public abstract class Player : MonoBehaviour
{
    private Dictionary<string, IStateBehavior> behaviorMap;
    private IStateBehavior currentBehavior;

    protected Animator animator;

    protected NavMeshAgent agent;
    protected Transform target;

    private string[] animationName = { "Seed", "CarrotsFarm", "GrassFarm" };

    private bool behaviorIsStop;

    //protected delegate void BehaviorDelegate();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        InitBehaviors();
    }
    private void InitBehaviors()
    {
        behaviorMap = new Dictionary<string, IStateBehavior>();

        behaviorMap[nameof(WalkBehavior)] = new WalkBehavior(agent, animator);

        foreach(string animName in animationName)
        {
            behaviorMap[animName] = new AnimBehavior(animator, animName);
            Debug.Log(animName);
        }
           
    }

    protected void SetBehavior(IStateBehavior stateBehavior)
    {
        if (currentBehavior != null && !behaviorIsStop)
            currentBehavior.Exit();

        currentBehavior = stateBehavior;
        currentBehavior.Enter(target);
        behaviorIsStop = false;
    }

    protected void StopBehavior()
    {
        if (currentBehavior != null)
        {
            currentBehavior.Exit();
            behaviorIsStop = true;
        }         
    }

    private IStateBehavior GetBehavior<T>() where T : IStateBehavior
    {
        var type = typeof(T);
        string nameType = type.ToString();
        Debug.Log(nameType);
        return behaviorMap[nameType];
    }

    private IStateBehavior GetBehavior(string nameBehavior)
    {
        Debug.Log(nameBehavior);
        return behaviorMap[nameBehavior];
    }

    protected void SetBehaviorWalk()
    {
      
        var behavior = GetBehavior<WalkBehavior>();
        SetBehavior(behavior);
    }

    protected void SetBehaviorAnim(string animTriggerName)
    {
        if(behaviorMap.ContainsKey(animTriggerName))
        {
            var behavior = GetBehavior(animTriggerName);
            SetBehavior(behavior);
        }
    }
}
