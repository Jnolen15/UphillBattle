using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitClass : MonoBehaviour
{
    public int hp;
    public int dmg;
    public float attackCooldown;
    public float movSpeed;

    public void MoveTo(Vector3 pos)
    {
        gameObject.GetComponent<UnitControl>().SetDestination(new Vector3(pos.x, transform.position.y, pos.z));
        //gameObject.GetComponent<Rigidbody>().MovePosition(newPos);
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
            Destroy(this.gameObject);
    }

    public abstract void Attack(GameObject target);
}
