using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitFunctions/Turn/GladiatorBuff")]
public class GladiatorBuff : UnitFunction
{
    public override void Activate(TokenUnit tUnit)
    {
        var player = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>();

        var damage = (tUnit.unit.attack + player.Courage);
        if (damage < tUnit.unit.attack)
            damage = tUnit.unit.attack;

        tUnit.TDamage = damage;
    }
}
