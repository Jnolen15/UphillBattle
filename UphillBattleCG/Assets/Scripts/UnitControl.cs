using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitControl : MonoBehaviour
{
    private TokenSlot ts;
    private TokenUnit tUnit;
    private CardManager cManager;

    private void Start()
    {
        ts = this.GetComponent<TokenSlot>();
        cManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CardManager>();
    }

    public void SetUpUnit(TokenUnit tu)
    {
        tUnit = tu;
    }

    public void RemoveUnit()
    {
        if(ts.type == TokenSlot.Type.Friendly)
            cManager.ReclaimFromBoard(tUnit.GetComponent<TokenUnit>().unit);

        tUnit = null;
    }


    public void AttackToken()
    {
        tUnit.OnAttack();
    }

    public void OnTurnToken()
    {
        tUnit.OnTurn();
    }

    public void DamageToken(int dmg)
    {
        tUnit.TakeDamage(dmg);
    }
}
