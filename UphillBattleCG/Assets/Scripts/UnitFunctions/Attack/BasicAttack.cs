using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitFunctions/Attacks/BasicAttack")]
public class BasicAttack : UnitFunction
{
    public bool isEnemy;

    public override void Activate(TokenUnit tUnit)
    {
        Debug.Log(tUnit.unit.title + " is attacking for " + tUnit.TDamage);

        var target = tUnit.boardManager.GetOpposingFrontline(tUnit.tokenSlot);
        if(target == null)
            target = tUnit.boardManager.GetOpposingBackline(tUnit.tokenSlot);

        if(target != null)
        {
            target.GetComponent<TokenUnit>().TakeDamage(tUnit.TDamage);

            // Slash effect
            Quaternion rot = Quaternion.identity;
            var slash = Instantiate(Resources.Load<GameObject>("Slash"), target.transform.position, rot, tUnit.gameObject.transform.parent.parent);
            slash.GetComponent<SlashEffect>().Animate(isEnemy);
            
            Debug.Log("Hit " + target.GetComponent<TokenUnit>().unit.name + " for " + tUnit.TDamage);
        }
        else
        {
            Debug.Log("Nobody to hit");
        }
    }
}
