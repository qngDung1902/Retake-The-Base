using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System.Dynamic;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterAnimator), typeof(Stats))]
public class Unit : MonoBehaviour
{
    public string StateName;
    protected StateMachine StateMachine;

    [HideInInspector] public NavMeshAgent Agent;
    [HideInInspector] public CharacterAnimator Animator;
    [HideInInspector] public Stats Stats;

    public IdleState IdleState;
    public MoveState MoveState;
    public ChaseState ChaseState;
    public AttackState AttackState;
    public DeadState DeadState;

    public virtual void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<CharacterAnimator>();
        Stats = GetComponent<Stats>();

        // InitializeState();
    }

    private void Update()
    {
        if (IsInState(DeadState)) return;
        StateMachine?.CurrentState?.LogicUpdate();
    }

    public virtual void InitializeState(bool noStartState = false)
    {
        IdleState = new(this);
        MoveState = new(this);
        ChaseState = new(this);
        AttackState = new(this);
        DeadState = new(this);

        StateMachine = new StateMachine();
        StateMachine.Initialize(noStartState ? null : IdleState);
    }

    public void ChangeState(State state)
    {
        if (IsInState(DeadState)) return;
        StateMachine.ChangState(state);
    }

    public bool IsInState(State state)
    {
        return StateMachine.IsInState(state);
    }

    public bool Attack()
    {
        if (IsInState(DeadState)) return false;
        return AttackState.CurrentTarget.Stats.Damaged(Stats.Atk);
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
