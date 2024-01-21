using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : UnitState
{
    public Unit ChasedTarget;
    public ChaseState(Unit unit) : base(unit) { }

    public override void Enter()
    {
        base.Enter();
        Unit.Animator.SetAnimation(ANIMATION.WALK);
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
            Unit.ChangeState(Unit.MeleeAttackState);
        }
    }
}
