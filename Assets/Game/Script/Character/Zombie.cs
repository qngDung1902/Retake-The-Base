using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Unit
{
    public SkinnedMeshRenderer MeshRenderer;
    public override void Awake()
    {
        base.Awake();
        InitializeState();
    }

    public void BakeMesh(Mesh mesh)
    {
        MeshRenderer.sharedMesh = mesh;
    }
}
