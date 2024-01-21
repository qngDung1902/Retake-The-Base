using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZombieHorde : MonoBehaviour
{
    public List<Mesh> ZombieMeshes;
    public Zombie Zombie;
    public Grid Grid;

    List<Zombie> Zombies = new();

    private void Awake()
    {
        Grid.Initialize();
        foreach (var position in Grid.Points)
        {
            var spawnPosition = position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            var zombie = Instantiate(Zombie, spawnPosition, Quaternion.Euler(0, Random.Range(-90, 90), 0), WorldSpace.Transform);
            zombie.BakeMesh(ZombieMeshes[Random.Range(0, ZombieMeshes.Count)]);
            zombie.SetHorde(this, spawnPosition);
            Zombies.Add(zombie);
        }
    }

    Dictionary<Collider, Unit> allies = new();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ally") || other.CompareTag("Player"))
        {
            allies.Add(other, other.GetComponent<Unit>());
            foreach (var zombie in Zombies)
            {
                if (!zombie.IsInState(zombie.ChaseState))
                {
                    TargetingAndRetarget(zombie);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ally") || other.CompareTag("Player"))
        {
            var unit = allies[other];
            allies.Remove(other);
            foreach (var zombie in Zombies)
            {
                if (zombie.ChaseState.ChasedTarget == unit)
                {
                    TargetingAndRetarget(zombie);
                }
            }
        }
    }

    public void TargetingAndRetarget(Zombie zombie)
    {
        var closestUnit = allies.OrderBy(e => (e.Value.transform.position - zombie.transform.position).sqrMagnitude).FirstOrDefault();
        if (closestUnit.Value)
        {
            zombie.ChangeState(zombie.ChaseState.SetTarget(closestUnit.Value));
        }
        else
        {
            zombie.ChangeState(zombie.MoveState.SetDestination(zombie.SpawnPosition));
        }
    }


}
