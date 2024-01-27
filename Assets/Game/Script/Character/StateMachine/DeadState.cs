using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : UnitState
{
    public DeadState(Unit unit) : base(unit) { }
    public override void Enter()
    {
        base.Enter();
        Unit.Animator.SetRandomAnimation(ANIMATION.DEAD);
    }
}
