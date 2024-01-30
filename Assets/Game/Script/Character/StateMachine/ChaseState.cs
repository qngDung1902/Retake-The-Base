using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : UnitState
{
    public Unit ChaseTarget;
    protected string animationName;
    public ChaseState(Unit unit) : base(unit)
    {
        animationName = ANIMATION.WALK;
    }

    public override void Enter()
    {
        base.Enter();
        Unit.Animator.SetAnimation(animationName);
        Unit.chaseTarget = ChaseTarget;
        // Debug.Log($"{Unit.Agent.remainingDistance}|{Unit.Agent.stoppingDistance}");
        // if (Unit.ReachedDestinationOrGaveUp())
        // {
        //     Unit.ChangeState(Unit.AttackState);
        // }
        // else
        // {
        // }
        // Unit.Agent.isStopped = false;
    }

    public override void Exit()
    {
        base.Exit();
        ChaseTarget = null;
        // Unit.Agent.isStopped = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // if (ChaseTarget == null)
        // {
        //     Unit.TargetingClosestTarget();
        //     return;
        // }

        if (!ChaseTarget || ChaseTarget.IsDead)
        {
            Unit.ChangeState(Unit.IdleState);
            Unit.TargetingClosestTarget();
            return;
        }

        Unit.Agent.SetDestination(ChaseTarget.transform.position);
        if (Unit.ReachedDestinationOrGaveUp())
        {
            Unit.ChangeState(Unit.AttackState.SetTarget(ChaseTarget));
        }
    }
}
