using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActionFunctions/DamageRow")]
public class DamageRow : ActionFunction
{
    public int ammount;
    public override void Activate(GameObject target)
    {
        var bMan = target.GetComponentInParent<BoardManager>();

        List<GameObject> rowList = new List<GameObject>();
        rowList = bMan.GetRow(target.GetComponent<TokenSlot>());

        foreach (GameObject token in rowList)
        {
            if (token.GetComponent<TokenSlot>().HasToken())
            {
                token.GetComponent<UnitControl>().DamageToken(ammount);

                // Slash effect
                Quaternion rot = Quaternion.identity;
                var slash = Instantiate(Resources.Load<GameObject>("Slash"), token.transform.position, rot, token.gameObject.transform.parent.parent);
                slash.GetComponent<SlashEffect>().Animate(false);
            }
        }
    }
}
