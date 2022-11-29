using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UnitFunctions/Attacks/Debuff")]
public class Debuff : UnitFunction
{
    public override void Activate(TokenUnit tUnit)
    {
        var target = tUnit.boardManager.GetOpposingFrontline(tUnit.tokenSlot);
        if (target == null)
            target = tUnit.boardManager.GetOpposingBackline(tUnit.tokenSlot);

        if (target != null)
        {
            target.GetComponent<TokenUnit>().TDamage -= 1;

            // Slash effect
            Quaternion rot = Quaternion.identity;
            var slash = Instantiate(Resources.Load<GameObject>("Slash"), target.transform.position, rot, target.gameObject.transform.parent.parent);
            slash.GetComponent<Image>().color = Color.magenta;
            slash.GetComponent<SlashEffect>().Animate(true);
        }
    }
}
