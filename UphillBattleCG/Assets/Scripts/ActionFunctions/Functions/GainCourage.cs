using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActionFunctions/GainCourage")]
public class GainCourage : ActionFunction
{
    public int ammount;

    public override void Activate(GameObject target)
    {
        var pMan = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>();

        pMan.Courage += ammount;
    }
}
