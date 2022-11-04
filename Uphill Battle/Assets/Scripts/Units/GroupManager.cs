using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManager : MonoBehaviour
{
    public float age;
    public float size;
    public int tieBreaker;
    public List<GameObject> members = new List<GameObject>();

    [SerializeField] private float formUpCooldown = 2f;
    [SerializeField] private bool formedUp;

    private void Awake()
    {
        tieBreaker = Random.Range(1, 1000);
    }

    void Update()
    {
        // Update age and size
        age += Time.deltaTime;

        if(size != members.Count)
            size = members.Count;

        // Update pos
        transform.position = new Vector3(members[0].transform.position.x, transform.position.y, members[0].transform.position.z);

        // Form up
        var allIdle = true;
        foreach (GameObject mem in members)
        {
            if (UnitControl.State.Idle != mem.GetComponent<UnitControl>().GetState())
            {
                allIdle = false;
                formedUp = false;
            }
        }
            
        if (allIdle && !formedUp)
            FormUp();
    }

    public void FormUp()
    {
        formedUp = true;

        List<Vector3> positions = new List<Vector3>();

        var rows = Mathf.CeilToInt(Mathf.Sqrt(members.Count));
        for (int x = 0; x < rows; x++)
        {
            var zpos = transform.position.z + (x - (rows / 2));

            for (int z = 0; z < rows; z++)
            {
                var xpos = transform.position.x + (z - (rows / 2));

                positions.Add(new Vector3(xpos, 1, zpos));

                if (positions.Count == members.Count)
                {
                    x = rows;
                    z = rows;
                }
            }
        }

        Debug.Log("Forming Up");
        int count = 0;
        foreach (GameObject unit in members.ToArray())
        {
            if (unit != null)
                unit.GetComponent<UnitClass>().MoveTo(positions[count]);
            else
            {
                Debug.LogError("null Member");
                members.Remove(unit);
            }

            count++;
        }
    }

    public void AddMember(GameObject newMem)
    {
        members.Add(newMem);
    }
    
    public void RemoveMember(GameObject oldMem)
    {
        members.Remove(oldMem);
    }

    public void MergeGroup(GroupManager otherGroup)
    {
        Debug.Log("Merging");

        // Compare groups
        if (otherGroup.size > size)
            otherGroup.AssimilateGroup(this);
        else
        {
            if (otherGroup.age > age)
                otherGroup.AssimilateGroup(this);
            else if (otherGroup.age == age)
            {
                if (otherGroup.tieBreaker > tieBreaker)
                    otherGroup.AssimilateGroup(this);
                else if (otherGroup.tieBreaker == tieBreaker)
                    Debug.LogError("GROUPS HAVE THE SAME AGE, SIZE, AND TIEBREAKER");
            }
        }
    }

    public void AssimilateGroup(GroupManager otherGroup)
    {
        Debug.Log("Assimilating");

        foreach (GameObject mem in otherGroup.members)
        {
            if(!members.Contains(mem))
                members.Add(mem);
        }

        Destroy(otherGroup.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "group")
        {
            Debug.Log("other group detected");
            MergeGroup(other.gameObject.GetComponent<GroupManager>());
        }
    }
}
