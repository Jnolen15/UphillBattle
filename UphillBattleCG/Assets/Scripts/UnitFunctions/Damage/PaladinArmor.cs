using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitFunctions/Damage/PaladinArmor")]
public class PaladinArmor : UnitFunction
{
    public override void Activate(TokenUnit tUnit)
    {
        if(tUnit.THealth < tUnit.unit.health)
            tUnit.TArmor += 1;
    }
}
