using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitFunctions/Attacks/EnemyAttack")]
public class EnemyAttack : UnitFunction
{
    public override void Activate(TokenUnit tUnit)
    {
        var data = tUnit.unit;
        Debug.Log(data.title + " is attacking Player for " + data.attack);

        var target = tUnit.boardManager.GetOpposingFrontline(tUnit.tokenSlot);
        if (target == null)
            target = tUnit.boardManager.GetOpposingBackline(tUnit.tokenSlot);

        if (target == null)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>().Health -= data.attack;
            Debug.Log("Hit for " + data.attack + " now " + GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>().Health);
        }
    }
}
