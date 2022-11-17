using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public List<GameObject> Hand = new List<GameObject>();
    public List<GameObject> CardSlots = new List<GameObject>();
    public List<GameObject> CurCardSlots = new List<GameObject>();

    private RectTransform rectTrans;
    private CardManager cardManager;

    private void Awake()
    {
        rectTrans = this.transform.GetComponent<RectTransform>();
        cardManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CardManager>();
    }

    // Adds a card to the player's hand
    public void AddCard(GameObject card)
    {
        Hand.Add(card);

        foreach(GameObject slot in CardSlots)
        {
            if (slot.GetComponent<HandSlot>().slotedCard == null)
            {
                CardSlots[CardSlots.IndexOf(slot)].GetComponent<HandSlot>().SlotCard(card);
                CurCardSlots.Add(CardSlots[CardSlots.IndexOf(slot)]);

                break;
            }
        }

        DefaultSlotPositions();
        card.SetActive(true);
    }

    // Removes a card from the player's hand
    public void Discard(GameObject cardSlot, GameObject card)
    {
        // Discard card
        Hand.Remove(card);
        cardManager.Discard(card);

        // De-activate slot
        cardSlot.transform.localPosition = Vector3.zero;
        CurCardSlots.Remove(cardSlot);
        DefaultSlotPositions();
    }

    // Evenly space out cards in hand, centered at the middle
    public void DefaultSlotPositions()
    {
        var count = 0;
        foreach (GameObject slot in CurCardSlots)
        {
            var xPos = 0f;
            xPos += 200*count;
            if(CurCardSlots.Count%2 != 1)
                xPos -= (200 * (CurCardSlots.Count/2)) -100;
            else
                xPos -= 200 * (CurCardSlots.Count / 2);
            var pos = new Vector3(xPos, 0, 0);
            slot.transform.localPosition = pos;
            count++;
        }
    }

    // Scoot left and right cards over to make room for highlighted card
    public void HighlightCard(GameObject highlightedSlot)
    {
        var pos = CurCardSlots.IndexOf(highlightedSlot);
        var left = pos - 1;
        var right = pos + 1;

        if (left > -1)
        {
            var slotPos = CurCardSlots[left].transform.localPosition;
            CurCardSlots[left].transform.localPosition = new Vector3(slotPos.x - 100, slotPos.y);
        }
            
        if (right < CurCardSlots.Count)
        {
            var slotPos = CurCardSlots[right].transform.localPosition;
            CurCardSlots[right].transform.localPosition = new Vector3(slotPos.x + 100, slotPos.y);
        }
    }
}
