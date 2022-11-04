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
            unitCont.AttackTarget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == unitCont.target && unitCont.hasAgro)
        {
            Debug.Log("Unit left attack, clearing");
            unitCont.ClearTarget();
            unitCont.ChangeState(UnitControl.State.Idle);
        }
    }
}
