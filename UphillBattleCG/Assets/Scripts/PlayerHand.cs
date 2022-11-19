using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerHand : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
                CardSlots[CardSlots.IndexOf(slot)].transform.SetAsLastSibling();

                break;
            }
        }

        DefaultSlotPositions();
        card.SetActive(true);
    }

    // Removes a card from the player's hand
    public void Discard(GameObject cardSlot, GameObject card, bool PlayedToBoard)
    {
        // Discard card
        Hand.Remove(card);
        if(PlayedToBoard)
            cardManager.PlayedToBoard(card);
        else
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
            slot.GetComponent<HandSlot>().defaultPos = pos;
            slot.GetComponent<HandSlot>().AnimateMovement(pos, new Vector2(1, 1));
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
            var defaultSlotPos = CurCardSlots[left].GetComponent<HandSlot>().defaultPos;
            CurCardSlots[left].GetComponent<HandSlot>().AnimateMovement(new Vector3(defaultSlotPos.x - 100, 0), new Vector2(1, 1));
        }
            
        if (right < CurCardSlots.Count)
        {
            var defaultSlotPos = CurCardSlots[right].GetComponent<HandSlot>().defaultPos;
            CurCardSlots[right].GetComponent<HandSlot>().AnimateMovement(new Vector3(defaultSlotPos.x + 100, 0), new Vector2(1, 1));
        }
    }

    // Raise hand
    public void OnPointerEnter(PointerEventData eventData)
    {
        AnimateHandMovement(new Vector2(0, 100));
    }

    // Lower hand
    public void OnPointerExit(PointerEventData eventData)
    {
        AnimateHandMovement(new Vector2(0, -60));
    }

    public void AnimateHandMovement(Vector3 newPos)
    {
        StopAllCoroutines();
        StartCoroutine(LerpPos(newPos));
    }

    IEnumerator LerpPos(Vector3 endPos)
    {
        float time = 0;
        float duration = 0.25f;
        Vector3 startPos = this.rectTrans.anchoredPosition;

        while (time < duration)
        {
            float t = time / duration;
            t = t * t * (3f - 2f * t);
            this.rectTrans.anchoredPosition = Vector3.Lerp(startPos, endPos, t);
            time += Time.deltaTime;
            yield return null;
        }

        this.rectTrans.anchoredPosition = endPos;
    }
}
