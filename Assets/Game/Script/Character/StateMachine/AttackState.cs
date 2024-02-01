using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : UnitState
{
    public Unit CurrentTarget;
    protected string animationName;

    public AttackState(Unit unit) : base(unit)
    {
        animationName = unit.KeyAttackAnimation;
    }

    public override void Enter()
    {
        base.Enter();
        Unit.Animator.SetAnimation(animationName);
    }
}
