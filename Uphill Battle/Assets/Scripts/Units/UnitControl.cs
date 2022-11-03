using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitControl : MonoBehaviour
{
    public bool isEnemy;

    private UnitClass unit;
    [SerializeField] private bool destinationReached;
    [SerializeField] private Vector3 destination;
    [SerializeField] private float cooldown;
    private GameObject SelectionIdentifier;

    private void Start()
    {
        unit = this.GetComponent<UnitClass>();
        
        if(!isEnemy)
            SelectionIdentifier = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        // MOVEMENT
        if (!destinationReached)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, unit.movSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, destination) < 0.1f)
                destinationReached = true;
        }

        // COOLDOWNS
        if (cooldown > 0) cooldown -= Time.deltaTime;
    }

    public void SetDestination(Vector3 pos)
    {
        destinationReached = false;
        destination = pos;
    }

    public void ToggleSelected(bool toggle)
    {
        SelectionIdentifier.SetActive(toggle);
    }

    private void OnTriggerStay(Collider other)
    {
        // Enemy attack
        if (isEnemy)
        {
            if (other.gameObject.tag == "controlled" && cooldown <= 0)
            {
                unit.Attack(other.gameObject);
                cooldown = unit.attackCooldown;
            }
        }
        // Friendly attack
        else
        {
            if (other.gameObject.tag == "enemy" && cooldown <= 0)
            {
                unit.Attack(other.gameObject);
                cooldown = unit.attackCooldown;
            }
        }
    }
}
