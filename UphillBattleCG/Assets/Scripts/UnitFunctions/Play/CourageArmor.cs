using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitFunctions/Played/CourageArmor")]
public class CourageArmor : UnitFunction
{
    public override void Activate(TokenUnit tUnit)
    {
        var player = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>();

        tUnit.TArmor = player.Courage;
    }
}
