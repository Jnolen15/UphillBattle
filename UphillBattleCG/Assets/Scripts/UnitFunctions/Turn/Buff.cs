using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitFunctions/Turn/Buff")]
public class Buff : UnitFunction
{
    public int ammount;

    public override void Activate(TokenUnit tUnit)
    {
        tUnit.TDamage += ammount;
    }
}
