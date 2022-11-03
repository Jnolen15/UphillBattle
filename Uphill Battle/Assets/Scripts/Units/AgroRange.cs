using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgroRange : MonoBehaviour
{
    private UnitControl unitCont;

    private void Start()
    {
        unitCont = transform.GetComponentInParent<UnitControl>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (UnitControl.State.Idle == unitCont.GetState())
        {
            if (other.gameObject.tag == "enemy" && !unitCont.hasAgro)
            {
                Debug.Log("Enemy in Argo Range, Targeting");
                unitCont.SetTarget(other.gameObject);
            }
        }
    }
}
