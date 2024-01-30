using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieChaseState : ChaseState
{
    Zombie zombie;
    public ZombieChaseState(Unit unit) : base(unit)
    {
        zombie = unit as Zombie;
    }

    public override void LogicUpdate()
    {
        if (!ChaseTarget || ChaseTarget.IsDead)
        {
            zombie.ChangeState(zombie.IdleState);
            zombie.TargetingClosestSoldier();
            return;
        }

        base.LogicUpdate();
    }
}
