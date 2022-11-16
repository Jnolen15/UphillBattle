using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public List<GameObject> Hand = new List<GameObject>();

    public void AddCard(GameObject card)
    {
        Hand.Add(card);
        card.transform.SetParent(this.transform);
        card.SetActive(true);
    }
}
