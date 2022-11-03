using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    private UnitControl unitCont;

    private void Start()
    {
        unitCont = transform.GetComponentInParent<UnitControl>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == unitCont.target && unitCont.hasAgro)
        {
            Debug.Log("Targeted Enemy in Attack Range");
            unitCont.AttackTarget();
        }
    }
}
