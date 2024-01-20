using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System.Dynamic;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterAnimator))]
public class Unit : MonoBehaviour
{
    public string StateName;
    protected StateMachine StateMachine;
    [HideInInspector] public NavMeshAgent Agent;
    [HideInInspector] public CharacterAnimator Animator;

    public IdleState IdleState;
    public MoveState MoveState;

    public virtual void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<CharacterAnimator>();

        // InitializeState();
    }

    private void Update()
    {
        StateMachine?.CurrentState?.LogicUpdate();
    }

    public virtual void InitializeState(bool noStartState = false)
    {
        IdleState = new(this);
        MoveState = new(this);

        StateMachine = new StateMachine();
        StateMachine.Initialize(noStartState ? null : IdleState);
    }

    public virtual void Interact()
    {

    }

    public void ChangeState(State state)
    {
        StateMachine.ChangState(state);
    }

    public bool IsInState(State state)
    {
        return StateMachine.IsInState(state);
    }

    public bool ReachedDestinationOrGaveUp()
    {
        if (!Agent.pathPending)
        {
            if (Agent.remainingDistance <= Agent.stoppingDistance)
            {
                if (!Agent.hasPath || Agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
