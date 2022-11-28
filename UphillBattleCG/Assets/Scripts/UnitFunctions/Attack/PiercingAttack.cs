using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitFunctions/Attacks/PiercingAttack")]
public class PiercingAttack : UnitFunction
{
    public bool isEnemy;

    public override void Activate(TokenUnit tUnit)
    {
        Debug.Log(tUnit.unit.title + " is attacking for " + tUnit.TDamage);

        var target1 = tUnit.boardManager.GetOpposingFrontline(tUnit.tokenSlot);
        var target2 = tUnit.boardManager.GetOpposingBackline(tUnit.tokenSlot);

        if (target1 != null)
            attack(target1, tUnit);
        if (target2 != null)
            attack(target2, tUnit);
    }

    private void attack(GameObject target, TokenUnit tUnit)
    {
        target.GetComponent<TokenUnit>().TakeDamage(tUnit.TDamage);

        // Slash effect
        Quaternion rot = Quaternion.identity;
        var slash = Instantiate(Resources.Load<GameObject>("Slash"), target.transform.position, rot, tUnit.gameObject.transform.parent.parent);
        slash.GetComponent<SlashEffect>().Animate(isEnemy);

        Debug.Log("Hit " + target.GetComponent<TokenUnit>().unit.name + " for " + tUnit.TDamage);
    }
}
