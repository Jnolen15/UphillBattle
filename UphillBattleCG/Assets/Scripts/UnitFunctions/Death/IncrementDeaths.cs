using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitFunctions/Death/IncrementDeaths")]
public class IncrementDeaths : UnitFunction
{
    public override void Activate(TokenUnit tUnit)
    {
        var player = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>();
        player.Deaths++;
    }
}
