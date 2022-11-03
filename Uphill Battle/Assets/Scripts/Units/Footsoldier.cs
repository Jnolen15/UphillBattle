using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsoldier : UnitClass
{
    [Header("Footsoldier variables")]
    [SerializeField] private float cooldown;

    private void Update()
    {
        // COOLDOWNS
        if (cooldown > 0) cooldown -= Time.deltaTime;
    }

    public override void Attack(GameObject target)
    {
        // Damage target on cooldown
        if (cooldown <= 0)
        {
            target.GetComponent<UnitClass>().TakeDamage(dmg);
            cooldown = attackCooldown;
        }
    }
}
