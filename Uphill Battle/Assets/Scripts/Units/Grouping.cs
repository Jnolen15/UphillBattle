using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grouping : MonoBehaviour
{
    public GameObject groupprefab;
    public GameObject group;

    void Start()
    {
        var pos = new Vector3(transform.position.x, 5, transform.position.z);
        group = Instantiate(groupprefab, pos, Quaternion.identity);
        group.GetComponent<GroupManager>().AddMember(this.gameObject);
    }

    public void RemoveSelf()
    {
        group.GetComponent<GroupManager>().RemoveMember(this.gameObject);
    }
}
