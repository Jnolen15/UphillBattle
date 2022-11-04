using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgroRange : MonoBehaviour
{
    private UnitControl unitCont;
    public string tagToTarget;

    private void Start()
    {
        unitCont = transform.GetComponentInParent<UnitControl>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (UnitControl.State.Moving != unitCont.GetState())
        {
            if (other.gameObject.tag == tagToTarget)
            {
                if (!unitCont.hasAgro || !unitCont.target)
                {
                    unitCont.SetTarget(other.gameObject);
                }
                // If there is a closer enemy they get agro
                else if(other.gameObject != unitCont.target)
                {
                    var targetDist = Vector3.Distance(transform.position, unitCont.target.transform.position);
                    var otherDist = Vector3.Distance(transform.position, other.gameObject.transform.position);
                    if (targetDist > otherDist)
                    {
                        unitCont.SetTarget(other.gameObject);
                    }
                }

            }
        }
    }
}
