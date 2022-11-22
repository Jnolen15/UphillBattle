using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitFunctions/Death/AwardKills")]
public class AwardKills : UnitFunction
{
    public override void Activate(TokenUnit tUnit)
    {
        var player = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>();
        player.Kills++;
    }
}
