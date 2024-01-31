using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Collections;
using UnityEngine;
using System.Linq;

public class Soldier : Unit
{
    public override void Awake()
    {
        base.Awake();
        All.Add(this);
    }

    public override void InitializeState(bool noStartState = false)
    {
        base.InitializeState(noStartState);
        ChaseState = new SoldierChaseState(this);
    }

    public override void Dead()
    {
        if (IsDead) return;

        ChangeState(DeadState);
        Animator.SetAnimation(ANIMATION.DEAD);
        Soldier.All.Remove(this);
        base.Dead();
    }

    public override void AttackEvent()
    {
        if (IsDead) return;
        if (!AttackState.CurrentTarget)
        {
            TargetClosestZombie();
            return;
        }

        AttackState.CurrentTarget.Stats.Damaged(this, Stats.Atk);
    }

    public override void AttackCompleteEvent()
    {
        if (AttackState.CurrentTarget.IsDead)
        {
            TargetClosestZombie();
            return;
        }
    }

    public void TargetClosestZombie()
    {
        if (ZombieHorde.All.Count == 0)
        {
            ChangeState(IdleState);
            return;
        }

        var closestTarget = ZombieHorde.All.First().Value.Zombies.OrderBy(n => (n.transform.position - transform.position).sqrMagnitude).FirstOrDefault();
        ChangeState(closestTarget ? ChaseState.SetTarget(closestTarget) : IdleState);
    }
    public static List<Soldier> All = new();
}
