using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedChaseState : ChaseState
{
    public RangedChaseState(Unit unit) : base(unit)
    {
        animationName = ANIMATION.RIFLE_CHASE;
    }

    public override void Enter()
    {
        base.Enter();
        Unit.Agent.stoppingDistance = 8f;
    }

    public override void Exit()
    {
        base.Exit();
        ChasedTarget = null;
    }

    // public override void LogicUpdate()
    // {
    //     if (ChasedTarget)
    //     {
    //         Unit.Agent.SetDestination(ChasedTarget.transform.position);
    //     }
    //     if (Unit.ReachedDestinationOrGaveUp())
    //     {
    //         Unit.ChangeState(Unit.AttackState);
    //     }
    // }
}
