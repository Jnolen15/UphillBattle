using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitControl : MonoBehaviour
{
    private UnitClass unit;
    [SerializeField] private Vector3 destination;
    private GameObject SelectionIdentifier;
    private GameObject attackRangeCol;
    private GameObject agroRangeCol;
    public GameObject target;
    public bool hasAgro;

    [SerializeField] private GameObject stateText;

    public enum State
    {
        Idle,
        Moving,
        Attack,
        Agro
    }
    private State state;
    public State GetState() { return state; }

    private void Start()
    {
        unit = this.GetComponent<UnitClass>();

        SelectionIdentifier = transform.GetChild(0).gameObject;

        attackRangeCol = transform.GetChild(1).gameObject;
        attackRangeCol.transform.localScale = new Vector3(unit.attackRange, unit.attackRange, unit.attackRange);

        agroRangeCol = transform.GetChild(2).gameObject;
        agroRangeCol.transform.localScale = new Vector3(unit.agroRange, unit.agroRange, unit.agroRange);
    }

    private void Update()
    {
        UpdateStateText();

        Debug.DrawRay(transform.position, transform.forward * 5, Color.red);

        switch (state)
        {
            default:
            case State.Idle: 
                //Nothing
                break;
            case State.Moving:
                LookTo(destination);
                transform.position = Vector3.MoveTowards(transform.position, destination, unit.movSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, destination) < 0.1f)
                    ChangeState(State.Idle);
                break;
            case State.Attack:
                if (target != null)
                {
                    LookTo(target.transform.position);
                    unit.Attack(target);
                }
                else
                {
                    hasAgro = false;
                    ChangeState(State.Idle);
                }
                break;
            case State.Agro:
                if (target != null)
                {
                    LookTo(target.transform.position);
                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, unit.movSpeed * Time.deltaTime);
                }
                else
                {
                    hasAgro = false;
                    ChangeState(State.Idle);
                }
                break;
        }
    }

    public void SetDestination(Vector3 pos)
    {
        ChangeState(State.Moving);
        destination = pos;
        ClearTarget();
    }

    private void LookTo(Vector3 target)
    {
        Vector3 targetPos = new Vector3(target.x, transform.position.y, target.z);
        transform.LookAt(targetPos, Vector3.up);
    }

    public void ToggleSelected(bool toggle)
    {
        SelectionIdentifier.SetActive(toggle);
    }

    public void ChangeState(State newState)
    {
        switch (newState)
        {
            default:
            case State.Idle: state = State.Idle; break;
            case State.Moving: state = State.Moving; break;
            case State.Attack: state = State.Attack; break;
            case State.Agro: state = State.Agro; break;
        }
    }

    public void SetTarget(GameObject newtarget)
    {
        target = newtarget;
        hasAgro = true;
        ChangeState(State.Agro);
    }

    public void AttackTarget()
    {
        ChangeState(State.Attack);
    }

    public void ClearTarget()
    {
        target = null;
        hasAgro = false;
    }

    private void UpdateStateText()
    {
        stateText.GetComponent<TextMeshPro>().text = state.ToString();
    }
}
