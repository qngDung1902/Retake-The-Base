using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : MoveState
{
    public PlayerMoveState(Unit unit) : base(unit) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate() { }
}
