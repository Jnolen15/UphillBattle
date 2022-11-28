using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitFunctions/Attacks/EnemyAttack")]
public class EnemyAttack : UnitFunction
{
    public override void Activate(TokenUnit tUnit)
    {
        Debug.Log(tUnit.unit.title + " is attacking Player for " + tUnit.TDamage);

        var target = tUnit.boardManager.GetOpposingFrontline(tUnit.tokenSlot);
        if (target == null)
            target = tUnit.boardManager.GetOpposingBackline(tUnit.tokenSlot);

        if (target == null)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>().Health -= tUnit.TDamage;
            Debug.Log("Hit for " + tUnit.TDamage + " now " + GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>().Health);

            // Slash effect
            target = tUnit.boardManager.GetOpposingEmpty(tUnit.tokenSlot);
            Quaternion rot = Quaternion.identity;
            var slash = Instantiate(Resources.Load<GameObject>("Slash"), target.transform.position, rot, target.gameObject.transform.parent.parent);
            slash.GetComponent<SlashEffect>().Animate(true);
        }
    }
}
