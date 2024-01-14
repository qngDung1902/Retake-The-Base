using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public bool canChangeState = true;
    public State CurrentState { get; private set; }

    public void Initialize(State startState)
    {
        canChangeState = true;
        CurrentState = startState;
        startState.Enter();
    }

    public void ChangState(State nextState)
    {
        if (!canChangeState) return;

        CurrentState?.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }

    public bool IsInState(State thisState) => CurrentState == thisState;
}

public abstract class State
{
    public virtual void Enter() { }
    public virtual void Exit() { }

    public virtual void LogicUpdate() { }
}

public class UnitState : State
{
    protected Unit Unit;
    public UnitState(Unit unit)
    {
        this.Unit = unit;
    }

    public override void Enter()
    {
        base.Enter();
        Unit.StateName = this.ToString();
    }
}
