using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActionFunctions/GiveAttack")]
public class GiveAttack : ActionFunction
{
    public int ammount;

    public override void Activate(GameObject target)
    {
        target.GetComponent<UnitControl>().AddAttack(ammount);
    }
}
