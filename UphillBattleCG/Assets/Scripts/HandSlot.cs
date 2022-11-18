using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSlot : MonoBehaviour
{
    public Vector3 defaultPos;
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
            AnimateMovement(new Vector2(defaultPos.x, 160), new Vector2(1.5f, 1.5f));
            hand.HighlightCard(this.gameObject);
        }
        else
        {
            hand.DefaultSlotPositions();
        }

    }

    public void Discard()
    {
        slotedCard.transform.SetParent(this.transform.parent.parent);
        hand.Discard(this.gameObject, slotedCard);
        slotedCard = null;
    }

    public void AnimateMovement(Vector3 newPos, Vector3 newSize)
    {
        StopAllCoroutines();

        // Start new movement
        StartCoroutine(LerpPos(newPos, newSize));
    }

    IEnumerator LerpPos(Vector3 endPos, Vector3 endScale)
    {
        float time = 0;
        float duration = 0.15f;
        Vector3 startPos = transform.localPosition;
        Vector3 startScale = transform.localScale;

        while (time < duration)
        {
            transform.localPosition = Vector3.Lerp(startPos, endPos, time / duration);
            transform.localScale = Vector3.Lerp(startScale, endScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = endPos;
    }
}
