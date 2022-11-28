using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitFunctions/Turn/GainArmor")]
public class GainArmor : UnitFunction
{
    public int ammount;

    public override void Activate(TokenUnit tUnit)
    {
        tUnit.TArmor += ammount;
    }
}
