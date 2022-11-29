using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitFunctions/Attacks/AllSpaceAttack")]
public class AllSpaceAttack : UnitFunction
{

    public override void Activate(TokenUnit tUnit)
    {
        var target1 = tUnit.boardManager.GetOpposingFrontlineSlot(tUnit.tokenSlot);
        var target2 = tUnit.boardManager.GetOpposingBacklineSlot(tUnit.tokenSlot);

        List<GameObject> rowList = new List<GameObject>();
        rowList.AddRange(tUnit.boardManager.GetRow(target1.GetComponent<TokenSlot>()));
        rowList.AddRange(tUnit.boardManager.GetRow(target2.GetComponent<TokenSlot>()));

        foreach (GameObject token in rowList)
        {
            if(token.transform.childCount > 0)
                Attack(token.transform.GetChild(0).gameObject, tUnit);
        }
    }

    private void Attack(GameObject target, TokenUnit tUnit)
    {
        if (target != null)
        {
            target.GetComponent<TokenUnit>().TakeDamage(tUnit.TDamage);

            // Slash effect
            Quaternion rot = Quaternion.identity;
            var slash = Instantiate(Resources.Load<GameObject>("Slash"), target.transform.position, rot, tUnit.gameObject.transform.parent.parent);
            slash.GetComponent<SlashEffect>().Animate(true);

            Debug.Log("Hit " + target.GetComponent<TokenUnit>().unit.name + " for " + tUnit.TDamage);
        }
        else
        {
            Debug.Log("Nobody to hit");
        }
    }
}
