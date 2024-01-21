using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHorde : MonoBehaviour
{
    public List<Mesh> ZombieMeshes;
    public Zombie Zombie;
    public Grid Grid;

    private void Awake()
    {
        Grid.Initialize();
        foreach (var position in Grid.Points)
        {
            Instantiate(Zombie, position, Quaternion.Euler(0, Random.Range(-90, 90), 0), WorldSpace.Transform).BakeMesh(ZombieMeshes[Random.Range(0, ZombieMeshes.Count)]);
        }
    }
}
