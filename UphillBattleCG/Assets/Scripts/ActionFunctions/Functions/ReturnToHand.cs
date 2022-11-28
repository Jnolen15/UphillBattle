using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActionFunctions/ReturnToHand")]
public class ReturnToHand : ActionFunction
{
    public override void Activate(GameObject target)
    {
        var cMan = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CardManager>();
        var data = target.GetComponentInChildren<TokenUnit>().unit;
        target.GetComponent<TokenSlot>().UnslotToken();
        Destroy(target.transform.GetChild(0).gameObject);

        cMan.DrawSpecific(data);
    }
}
