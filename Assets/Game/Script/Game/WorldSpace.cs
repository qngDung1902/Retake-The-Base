using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpace : MonoBehaviour
{
    public static Transform Transform;
    private void Awake()
    {
        Transform = transform;
    }
}
