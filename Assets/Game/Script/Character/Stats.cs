using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float Atk;
    public float Hp;

    private Unit unit;

    private void Awake()
    {
        unit = GetComponent<Unit>();
    }

    public bool Damaged(float value)
    {
        Hp -= value;
        if (Hp < 0)
        {
            Hp = 0;
            unit.Dead();
            return true;
        }

        return false;
    }
}
