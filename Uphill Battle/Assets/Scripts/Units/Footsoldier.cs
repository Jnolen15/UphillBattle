using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsoldier : UnitClass
{
    private UnitControl uc;
    [Header("Footsoldier variables")]
    [SerializeField] private float cooldown;
    [SerializeField] private bool touchingEnemy;
    [SerializeField] private GameObject attackRangeCol;
    [SerializeField] private GameObject agroRangeCol;

    private void Start()
    {
        uc = GetComponent<UnitControl>();

        attackRangeCol = transform.GetChild(1).gameObject;
        attackRangeCol.transform.localScale = new Vector3(attackRange, attackRange, attackRange);

        agroRangeCol = transform.GetChild(2).gameObject;
        agroRangeCol.transform.localScale = new Vector3(agroRange, agroRange, agroRange);
    }

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
