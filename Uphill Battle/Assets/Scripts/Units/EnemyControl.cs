using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private UnitClass unit;

    [SerializeField] private float cooldown;

    private void Start()
    {
        unit = this.GetComponent<UnitClass>();
    }

    private void Update()
    {
        // COOLDOWNS
        if (cooldown > 0) cooldown -= Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "controlled" && cooldown <= 0)
        {
            unit.Attack(other.gameObject);
            cooldown = unit.attackCooldown;
        }
    }
}
