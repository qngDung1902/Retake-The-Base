using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : UnitState
{
    public IdleState(Unit unit) : base(unit) { }

    public override void Enter()
    {
        base.Enter();
        Unit.Animator.SetAnimation(ANIMATION.IDLE);
    }
}
