using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // ======== CARD PILES ========
    public List<GameObject> Deck = new List<GameObject>();
    public List<GameObject> Discards = new List<GameObject>();
    public List<GameObject> OnBoard = new List<GameObject>();

    public List<GameObject> UnlockableActionCards = new List<GameObject>();

    // ======== REFRENCES ========
    public PlayerHand hand;

    void Update()
    {
        // FOR TESTING RESAONS
        if (Input.GetKeyDown(KeyCode.D))
            DrawCards(1);
    }

    public void DrawCards(int num)
    {
        for(int i = num; i > 0; i--)
        {
            if (Deck.Count > 0)
            {
                if (hand.Hand.Count < 5)
                {
                    var card = GetRandomCard();
                    Deck.Remove(card);
                    hand.AddCard(card);
                }
                else
                {
                    Debug.Log("Hand Full");
                }
            }
            else
            {
                Debug.Log("Deck is empty");
                if (Discards.Count > 0)
                {
                    Debug.Log("Shuffling discards into deck");
                    ShuffleDiscards();
                    DrawCards(i);
                }
            }
        }
    }

    private GameObject GetRandomCard()
    {
        return (Deck[Random.Range(0, Deck.Count)]);
    }

    public void Discard(GameObject card)
    {
        Discards.Add(card);
        card.SetActive(false);
    }

    public void PlayedToBoard(GameObject card)
    {
        OnBoard.Add(card);
        card.SetActive(false);
    }

    public void ReclaimFromBoard(UnitSO data)
    {
        foreach(GameObject card in OnBoard)
        {
            if(data.title == card.GetComponent<CardUnit>().unit.title)
            {
                OnBoard.Remove(card);
                Discards.Add(card);
                break;
            }
        }
    }

    // Adds discard pile into deck pile
    private void ShuffleDiscards()
    {
        foreach(GameObject card in Discards)
        {
            Deck.Add(card);
        }

        Discards.Clear();
    }

    public GameObject GetUnlockableActionCard()
    {
        if(UnlockableActionCards.Count > 0)
        {
            int rand = Random.Range(0, UnlockableActionCards.Count);
            var chosenCard = UnlockableActionCards[rand];
            UnlockableActionCards.Remove(chosenCard);
            return chosenCard;
        }

        return null;
    }

    public void AddToDeck(GameObject newcard)
    {
        Deck.Add(newcard);
        Debug.Log("Added " + newcard);
    }
}
