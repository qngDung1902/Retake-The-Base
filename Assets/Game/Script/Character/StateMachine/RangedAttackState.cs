using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : AttackState
{
    public RangedAttackState(Unit unit) : base(unit)
    {
        animationName = ANIMATION.RIFLE_FIRE;
    }
}
