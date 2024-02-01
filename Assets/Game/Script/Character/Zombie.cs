using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Zombie : Unit
{
    [HideInInspector] public ZombieHorde Horde;
    [HideInInspector] public Vector3 SpawnPosition;

    public override void Awake()
    {
        base.Awake();
        InitializeState();
    }

    private void Start()
    {
        RandomStats();
    }

    public override void InitializeState(bool noStartState = false)
    {
        base.InitializeState(noStartState);
        AttackState = new ZombieMeleeAttackState(this);
        ChaseState = new ZombieChaseState(this);
    }

    public override void Dead()
    {
        if (IsDead) return;

        ChangeState(DeadState);
        Animator.SetRandomAnimation(ANIMATION.DEAD);
        Horde.UpdateZombieCount(this);
        base.Dead();
    }

    public override void Damaged(Unit source)
    {
        if (!IsInFight)
        {
            ChangeState(ChaseState.SetTarget(source));
        }
    }

    public override void AttackEvent()
    {
        if (IsDead) return;
        if (!AttackState.CurrentTarget)
        {
            TargetingClosestSoldier();
            return;
        }

        AttackState.CurrentTarget.Stats.Damaged(this, Stats.Atk);
    }

    public override void AttackCompleteEvent()
    {
        if (AttackState.CurrentTarget.IsDead)
        {
            TargetingClosestSoldier();
            return;
        }

        if (Agent.remainingDistance > Agent.stoppingDistance)
        {
            ChangeState(ChaseState.SetTarget(AttackState.CurrentTarget));
        }
    }

    public void SetHorde(ZombieHorde horde, Vector3 spawnPosition)
    {
        Horde = horde;
        SpawnPosition = spawnPosition;
    }

    public void WaitToRetarget()
    {
        Invoke(nameof(Retarget), 2.7f);

        void Retarget()
        {
            Horde.TargetingClosestTarget(this);
        }
    }

    public void BakeMesh(Mesh mesh)
    {
        MeshRenderer.sharedMesh = mesh;
    }

    void RandomStats()
    {
        Agent.speed = Random.Range(2f, 4f);
        Agent.angularSpeed = Random.Range(160, 200);
        Animator.SetAnimatorSpeed(Agent.speed / 3f);
    }

    public void TargetingClosestSoldier()
    {
        if (Soldier.All.Count == 0)
        {
            ChangeState(MoveState.SetDestination(SpawnPosition));
            return;
        }

        var closestTarget = Soldier.All.OrderBy(n => (n.transform.position - transform.position).sqrMagnitude).First();
        ChangeState(closestTarget ? ChaseState.SetTarget(closestTarget) : MoveState.SetDestination(SpawnPosition));
    }
}
