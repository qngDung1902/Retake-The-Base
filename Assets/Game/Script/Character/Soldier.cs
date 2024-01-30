using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Collections;
using UnityEngine;

public class Soldier : Unit
{
    public override void Awake()
    {
        base.Awake();
        All.Add(this);
    }



    public static List<Soldier> All = new();
}
