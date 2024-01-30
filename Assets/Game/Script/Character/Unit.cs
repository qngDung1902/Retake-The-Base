using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterAnimator), typeof(Stats))]
public class Unit : MonoBehaviour
{
    [Header("--- DEBUG ---")]
    public string StateName;
    public Unit chaseTarget;

    [Space(8)]
    [Header("--- REFERENCES ---")]
    public SkinnedMeshRenderer MeshRenderer;
    public GameObject Shadow;

    protected StateMachine StateMachine;
    [HideInInspector] public NavMeshAgent Agent;
    [HideInInspector] public CharacterAnimator Animator;
    [HideInInspector] public Stats Stats;

    public IdleState IdleState;
    public MoveState MoveState;
    public ChaseState ChaseState;
    public AttackState AttackState;
    public DeadState DeadState;

    public bool IsInFight => (IsInState(ChaseState) || IsInState(AttackState));
    public bool IsDead => IsInState(DeadState);

    public virtual void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<CharacterAnimator>();
        Stats = GetComponent<Stats>();

        // InitializeState();
    }

    private void Update()
    {
        if (IsDead) return;
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
        if (IsDead) return;
        StateMachine.ChangState(state);
    }

    public bool IsInState(State state)
    {
        return StateMachine.IsInState(state);
    }

    public virtual void Dead()
    {
        Animator.SetRandomAnimation(ANIMATION.DEAD);
        Shadow.SetActive(false);
        Agent.enabled = false;
    }

    public bool Attack()
    {
        if (IsDead) return false;
        bool killTarget = AttackState.CurrentTarget.Stats.Damaged(Stats.Atk);
        if (killTarget)
        {
            TargetingClosestTarget();
        }
        return killTarget;
    }

    public void TargetingClosestTarget()
    {
        if (ZombieHorde.All.Count == 0)
        {
            ChangeState(IdleState);
            return;
        }

        var closestTarget = ZombieHorde.All.First().Value.Zombies.OrderBy(n => (n.transform.position - transform.position).sqrMagnitude).First();
        if (closestTarget)
        {
            ChangeState(ChaseState.SetTarget(closestTarget));
        }
        else
        {
            ChangeState(IdleState);
        }
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
