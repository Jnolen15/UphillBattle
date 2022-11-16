using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // ======== CARD PILES ========
    public List<GameObject> Deck = new List<GameObject>();
    public List<GameObject> Discards = new List<GameObject>();

    // ======== REFRENCES ========
    public PlayerHand hand;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            DrawCard();
    }

    public void DrawCard()
    {
        if(Deck.Count > 0)
        {
            var card = GetRandomCard();
            Deck.Remove(card);
            hand.AddCard(card);
        }
        else
        {
            Debug.Log("Deck is empty");
        }
    }

    private GameObject GetRandomCard()
    {
        return (Deck[Random.Range(0, Deck.Count)]);
    }
}
