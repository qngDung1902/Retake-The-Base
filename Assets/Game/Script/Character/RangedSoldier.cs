using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSoldier : Soldier
{
    // public override void Awake()
    // {
    //     base.Awake();
    // }
    private void Start()
    {
        RandomStats();
    }

    public override void InitializeState(bool noStartState = false)
    {
        base.InitializeState(noStartState);
        ChaseState = new RangedChaseState(this);
        AttackState = new RangedAttackState(this);
    }

    void RandomStats()
    {
        // Agent.stoppingDistance = 8f;
        Agent.speed = Random.Range(2.5f, 3.5f);
        Agent.angularSpeed = Random.Range(120, 200);
        Animator.SetAnimatorSpeed(Agent.speed / 3f);
    }
}
