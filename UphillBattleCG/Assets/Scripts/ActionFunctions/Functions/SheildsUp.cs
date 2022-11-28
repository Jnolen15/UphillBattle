using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActionFunctions/SheildsUp")]
public class SheildsUp : ActionFunction
{
    public override void Activate(GameObject target)
    {
        var bMan = target.GetComponentInParent<BoardManager>();

        List<GameObject> rowList = new List<GameObject>();
        rowList = bMan.GetRow(target.GetComponent<TokenSlot>());

        foreach(GameObject token in rowList)
        {
            if (token.GetComponent<TokenSlot>().HasToken())
            {
                token.GetComponent<UnitControl>().AddArmor(1);
            }
        }
    }
}
