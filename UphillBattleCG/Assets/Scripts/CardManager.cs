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
    public List<UnitSO> UpgradeCards = new List<UnitSO>();
    public List<GameObject> ExtraUnitCards = new List<GameObject>();

    // ======== REFRENCES ========
    public PlayerHand hand;
    public GameObject unitCardPrefab;

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
            if(data.tag == card.GetComponent<CardUnit>().unit.tag)
            {
                OnBoard.Remove(card);
                Discards.Add(card);
                break;
            }
        }
    }

    public void DrawSpecific(UnitSO data)
    {
        // Hackey solution for return to hand card sometimes not working right
        StartCoroutine(DrawACard(data));
    }

    IEnumerator DrawACard(UnitSO data)
    {
        yield return new WaitForSeconds(0.2f);

        Debug.Log("Looking for " + data.title);

        var foundCard = false;
        foreach (GameObject card in Deck)
        {
            var cur = card.GetComponent<CardUnit>();
            if (cur != null)
            {
                Debug.Log("Looking at " + card + ": " + cur.unit.title);
                if (data.tag == cur.unit.tag)
                {
                    Deck.Remove(card);
                    hand.AddCard(card);
                    foundCard = true;
                    break;
                }
            }
        }

        if (!foundCard)
        {
            foreach (GameObject card in Discards)
            {
                var cur = card.GetComponent<CardUnit>();
                if (cur != null)
                {
                    Debug.Log("Looking at " + card + ": " + cur.unit.title);
                    if (data.title == cur.unit.title)
                    {
                        Discards.Remove(card);
                        hand.AddCard(card);
                        foundCard = true;
                        break;
                    }
                }
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

    public void UpgradeUnits(UnitSO oldData, UnitSO newData)
    {
        UpgradeCardsInList(Deck, oldData, newData);
        UpgradeCardsInList(hand.Hand, oldData, newData);
        UpgradeCardsInList(Discards, oldData, newData);
        UpgradeCardsInList(OnBoard, oldData, newData);
    }

    private void UpgradeCardsInList(List<GameObject> curlist, UnitSO oldData, UnitSO newData)
    {
        foreach (GameObject card in curlist)
        {
            var cardUnit = card.GetComponent<CardUnit>();
            if (cardUnit)
            {
                if (cardUnit.unit == oldData)
                {
                    cardUnit.unit = newData;
                    cardUnit.SetUp();
                }
            }
        }
    }

    public void GetExtraUnitCard(UnitSO data)
    {
        foreach(GameObject card in ExtraUnitCards)
        {
            if (data.title == card.GetComponent<CardUnit>().unit.title)
            {
                ExtraUnitCards.Remove(card);
                Deck.Add(card);
                break;
            }
        }
    }
}
