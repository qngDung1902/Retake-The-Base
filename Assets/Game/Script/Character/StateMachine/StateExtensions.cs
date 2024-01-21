using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StateExtensions
{
    public static MoveState SetDestination(this MoveState moveState, Vector3 destination)
    {
        moveState.Destination = destination;
        return moveState;
    }

    public static ChaseState SetTarget(this ChaseState chaseState, Unit target)
    {
        chaseState.ChasedTarget = target;
        return chaseState;
    }
}
