using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : UnitState
{
    public MeleeAttackState(Unit unit) : base(unit) { }

    public override void Enter()
    {
        base.Enter();
        Unit.Animator.SetAnimation(ANIMATION.MELEE_ATTACK);
    }
}
