using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Unit
{
    public SkinnedMeshRenderer MeshRenderer;
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
        ChangeState(DeadState);
    }

    public override void InitializeState(bool noStartState = false)
    {
        base.InitializeState(noStartState);
        AttackState = new ZombieMeleeAttackState(this);
    }

    void RandomStats()
    {
        Agent.speed = Random.Range(1f, 4f);
        Agent.angularSpeed = Random.Range(80, 200);
        Animator.SetAnimatorSpeed(Agent.speed / 3f);
    }

    public void SetHorde(ZombieHorde horde, Vector3 spawnPosition)
    {
        Horde = horde;
        SpawnPosition = spawnPosition;
    }

    public void WaitToRetarget()
    {
        Invoke(nameof(Retarget), 2.7f);
    }

    public void BakeMesh(Mesh mesh)
    {
        MeshRenderer.sharedMesh = mesh;
    }

    void Retarget()
    {
        Horde.TargetingAndRetarget(this);
    }
}
