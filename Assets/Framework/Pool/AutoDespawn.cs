using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDespawn : PoolElement
{
    [SerializeField] float timeLife = 0.2f;
    private void OnEnable()
    {
        DespawnSelf(timeLife);
    }
}
