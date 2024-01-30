using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMeleeAttackState : AttackState
{
    Zombie Zombie;
    public ZombieMeleeAttackState(Unit unit) : base(unit)
    {
        Zombie = unit as Zombie;
    }

    // public override void LogicUpdate()
    // {
    //     base.LogicUpdate();
    //     if (CurrentTarget)
    //     {
    //         Unit.transform.LookAt(CurrentTarget.transform.position);
    //     }
    // }
}
