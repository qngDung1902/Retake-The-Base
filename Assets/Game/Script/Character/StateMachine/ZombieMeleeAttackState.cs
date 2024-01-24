using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMeleeAttackState : AttackState
{
    Zombie Zombie;
    public ZombieMeleeAttackState(Unit unit) : base(unit)
    {
        Zombie = Unit as Zombie;
    }

    public override void Enter()
    {
        base.Enter();
        Zombie.WaitToRetarget();
    }
}
