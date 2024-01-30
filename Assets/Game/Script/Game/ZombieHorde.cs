using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZombieHorde : MonoBehaviour
{
    public static SortedList<int, ZombieHorde> All = new();

    public int Id;
    public List<Mesh> ZombieMeshes;
    public Zombie Zombie;
    public Grid Grid;


    [HideInInspector] public List<Zombie> Zombies = new();
    public bool Clear => Zombies.Count == 0;

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

        // Debug.Log(All.Count);
        All.Add(Id, this);
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
                    TargetingClosestTarget(zombie);
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
                if (zombie.ChaseState.ChaseTarget == unit)
                {
                    TargetingClosestTarget(zombie);
                }
            }
        }
    }

    public void TargetingClosestTarget(Zombie zombie)
    {
        var closestUnit = allies.OrderBy(n => (n.Value.transform.position - zombie.transform.position).sqrMagnitude).FirstOrDefault();
        if (closestUnit.Value)
        {
            zombie.ChangeState(zombie.ChaseState.SetTarget(closestUnit.Value));
        }
        else
        {
            zombie.ChangeState(zombie.MoveState.SetDestination(zombie.SpawnPosition));
        }
    }


    public void UpdateZombieCount(Zombie zombie)
    {
        Zombies.Remove(zombie);
        if (Clear)
        {
            All.Remove(Id);
        }
    }
}
