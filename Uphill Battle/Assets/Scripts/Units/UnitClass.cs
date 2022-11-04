using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitClass : MonoBehaviour
{
    [Header("Unit variables")]
    public int hp;
    public int dmg;
    public float attackCooldown;
    public float attackRange;
    public float agroRange;
    public float movSpeed;

    public enum Type
    {
        Melee,
        Range
    }
    private Type type;
    public new Type GetType() { return type; }

    public void MoveTo(Vector3 pos)
    {
        gameObject.GetComponent<UnitControl>().SetDestination(new Vector3(pos.x, transform.position.y, pos.z));
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
            Die();
    }

    public abstract void Attack(GameObject target);

    public abstract void Die();
}
