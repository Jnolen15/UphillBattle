using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActionFunctions/Bounty")]
public class Bounty : ActionFunction
{
    public int ammount;

    public override void Activate(GameObject target)
    {
        var pMan = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>();

        // Gain a gold if it would die
        if (target.GetComponent<UnitControl>().GetTokenHealth() <= ammount)
        {
            pMan.Gold += 1;
        }

        target.GetComponent<UnitControl>().DamageToken(1);

        // Slash effect
        Quaternion rot = Quaternion.identity;
        var slash = Instantiate(Resources.Load<GameObject>("Slash"), target.transform.position, rot, target.gameObject.transform.parent.parent);
        slash.GetComponent<SlashEffect>().Animate(false);
    }
}
