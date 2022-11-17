using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSlot : MonoBehaviour
{
    public GameObject slotedCard;

    private PlayerHand hand;

    private void Awake()
    {
        hand = transform.parent.GetComponent<PlayerHand>();
    }

    void Update()
    {
        if(slotedCard)
            slotedCard.transform.position = this.transform.position;
    }

    public void SlotCard(GameObject card)
    {
        card.transform.SetParent(this.transform);
        slotedCard = card;
    }

    // Moves card up and makes it bigger, calls PlayerHand to scoot adjacent cards over
    public void HighlightSlot(bool toggle)
    {
        if (toggle)
        {
            this.transform.localScale = this.transform.localScale * 1.5f;
            this.transform.localPosition = new Vector2(transform.localPosition.x, 160);
            hand.HighlightCard(this.gameObject);
        }
        else
        {
            this.transform.localScale = this.transform.localScale / 1.5f;
            hand.DefaultSlotPositions();
        }

    }

    public void Discard()
    {
        //HighlightSlot(false);
        slotedCard.transform.SetParent(this.transform.parent.parent);
        hand.Discard(this.gameObject, slotedCard);
        slotedCard = null;
    }
}
