using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitControl : MonoBehaviour
{
    private TokenSlot ts;
    private TokenUnit tUnit;

    private void Start()
    {
        ts = this.GetComponent<TokenSlot>();
    }

    public void SetUpUnit(TokenUnit tu)
    {
        tUnit = tu;
    }

    
    public void AttackToken()
    {
        tUnit.Attack();
    }

    public void DamageToken(int dmg)
    {
        tUnit.TakeDamage(dmg);
    }
}
