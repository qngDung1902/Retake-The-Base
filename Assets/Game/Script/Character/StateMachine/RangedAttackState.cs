using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : AttackState
{
    public RangedAttackState(Unit unit) : base(unit)
    {
        // animationName = unit ;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (CurrentTarget)
        {
            Unit.transform.LookAt(CurrentTarget.transform.position);
        }
    }
}
