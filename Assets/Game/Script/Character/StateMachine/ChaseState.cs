using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : UnitState
{
    public Unit ChasedTarget;
    protected string animationName;
    public ChaseState(Unit unit) : base(unit)
    {
        animationName = ANIMATION.WALK;
    }

    public override void Enter()
    {
        base.Enter();
        // Debug.Log($"{Unit.Agent.remainingDistance}|{Unit.Agent.stoppingDistance}");
        // if (Unit.ReachedDestinationOrGaveUp())
        // {
        //     Unit.ChangeState(Unit.AttackState);
        // }
        // else
        // {
        Unit.Animator.SetAnimation(animationName);
        // }
        // Unit.Agent.isStopped = false;
    }

    public override void Exit()
    {
        base.Exit();
        ChasedTarget = null;
        // Unit.Agent.isStopped = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Unit.Agent.SetDestination(ChasedTarget.transform.position);
        if (Unit.ReachedDestinationOrGaveUp())
        {
            Unit.ChangeState(Unit.AttackState.SetTarget(ChasedTarget));
        }
    }
}
