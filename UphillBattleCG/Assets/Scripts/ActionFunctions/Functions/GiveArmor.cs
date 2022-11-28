using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActionFunctions/GiveArmor")]
public class GiveArmor : ActionFunction
{
    public int ammount;

    public override void Activate(GameObject target)
    {
        target.GetComponent<UnitControl>().AddArmor(ammount);
    }
}
