using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AttackOrder : MonoBehaviour
{
    public Slider Progress;
    Tween tween;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Attack();
            // tween = Progress.DOValue(Progress.maxValue, 1f).SetEase(Ease.Linear).OnComplete(Attack);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tween?.Kill();
            Progress.value = 0;
        }
    }

    void Attack()
    {
        Debug.Log($"[Game] Attack!");
        var hordeTarget = ZombieHorde.All[0];
        var targets = hordeTarget.Zombies.PickRandom(hordeTarget.Zombies.Count / 2).ToArray();
        Zombie target;

        foreach (var soldier in Soldier.All)
        {
            target = targets[Random.Range(0, targets.Length)]; ;
            soldier.ChangeState(soldier.ChaseState.SetTarget(target));
        }
    }
}
