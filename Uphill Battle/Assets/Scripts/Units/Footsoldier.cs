using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsoldier : UnitClass
{
    public override void Attack(GameObject target)
    {
        target.GetComponent<UnitClass>().TakeDamage(dmg);
    }
}
