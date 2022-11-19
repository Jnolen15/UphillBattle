using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitFunctions/Attacks/BasicAttack")]
public class BasicAttack : UnitFunction
{
    public override void Activate(TokenUnit tUnit)
    {
        var data = tUnit.unit;
        Debug.Log(data.title + " is attacking for " + data.damage);

        var target = tUnit.boardManager.GetOpposingFrontline(tUnit.tokenSlot);
        if(target == null)
            target = tUnit.boardManager.GetOpposingBackline(tUnit.tokenSlot);

        if(target != null)
        {
            target.GetComponent<TokenUnit>().TakeDamage(data.damage);
            Debug.Log("Hit " + target + " for " + data.damage);
        }
        else
        {
            Debug.Log("Nobody to hit");
        }
    }
}
