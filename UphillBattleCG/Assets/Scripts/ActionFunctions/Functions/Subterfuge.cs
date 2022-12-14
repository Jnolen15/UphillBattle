using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActionFunctions/Subterfuge")]
public class Subterfuge : ActionFunction
{
    public override void Activate(GameObject target)
    {
        var pMan = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>();
        pMan.Courage -= 1;

        target.GetComponent<UnitControl>().DamageToken(3);

        // Slash effect
        Quaternion rot = Quaternion.identity;
        var slash = Instantiate(Resources.Load<GameObject>("Slash"), target.transform.position, rot, target.gameObject.transform.parent.parent);
        slash.GetComponent<SlashEffect>().Animate(false);
    }
}
