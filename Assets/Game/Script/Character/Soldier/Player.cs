using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public override void Awake()
    {
        base.Awake();
        InitializeState();
    }

    public override void InitializeState(bool noStartState = false)
    {
        IdleState = new PlayerIdleState(this);
        MoveState = new PlayerMoveState(this);

        StateMachine = new StateMachine();
        StateMachine.Initialize(noStartState ? null : IdleState);
    }
}
