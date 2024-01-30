using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierChaseState : ChaseState
{
    Soldier soldier;
    public SoldierChaseState(Unit unit) : base(unit)
    {
        soldier = unit as Soldier;
        animationName = ANIMATION.RIFLE_CHASE;
    }

    public override void LogicUpdate()
    {
        if (!ChaseTarget || ChaseTarget.IsDead)
        {
            soldier.ChangeState(soldier.IdleState);
            soldier.TargetClosestZombie();
            return;
        }

        base.LogicUpdate();
    }
}
