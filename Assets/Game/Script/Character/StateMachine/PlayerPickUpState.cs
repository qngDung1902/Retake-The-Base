using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpState : UnitState
{
    public PlayerPickUpState(Unit unit) : base(unit) { }

    public override void Enter()
    {
        base.Enter();
        Unit.Animator.SetAnimation(ANIMATION.PICK_UP);
    }
}
