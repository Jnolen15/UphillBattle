using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitFunctions/Death/AwardCourage")]
public class AwardCourage : UnitFunction
{
    public int ammount;

    public override void Activate(TokenUnit tUnit)
    {
        var player = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>();
        Debug.Log("Awarding courage " + ammount);
        player.Courage += ammount;
    }
}
