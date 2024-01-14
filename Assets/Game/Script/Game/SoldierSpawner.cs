using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoldierSpawner : MonoBehaviour
{
    public Soldier Soldier;
    public Grid Grid;

    private void Awake()
    {
        Grid.Initialize();
        StartCoroutine(SpawningSolider());
    }

    IEnumerator SpawningSolider()
    {
        yield return new WaitForSeconds(2f);

        foreach (var position in Grid.Points)
        {
            yield return new WaitForSeconds(1f);
            var soldier = Instantiate(Soldier, transform.position, Quaternion.identity);
            soldier.InitializeState(true);
            soldier.ChangeState(soldier.MoveState.SetDestination(position));
        }
    }
}
