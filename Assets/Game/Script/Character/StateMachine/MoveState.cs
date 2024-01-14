using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class MoveState : UnitState
{
    public MoveState(Unit unit) : base(unit)
    {
        Unit.Agent.autoBraking = false;
    }

    public Vector3 Destination;


    public override void Enter()
    {
        base.Enter();
        Unit.Animator.SetAnimation(ANIMATION.WALK);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Unit.Agent.SetDestination(Destination);
        if (Unit.ReachedDestinationOrGaveUp())
        {
            Unit.ChangeState(Unit.IdleState);
        }
    }
}

public static class MoveStateExtensions
{
    public static MoveState SetDestination(this MoveState moveState, Vector3 destination)
    {
        moveState.Destination = destination;
        return moveState;
    }
}

