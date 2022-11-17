using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Card : MonoBehaviour, 
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.parent.GetComponent<HandSlot>().HighlightSlot(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.parent.GetComponent<HandSlot>().HighlightSlot(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Play();
        this.transform.parent.GetComponent<HandSlot>().Discard();
    }

    // OVERRIDE CLASSES
    public abstract void SetUp();

    public abstract void Play();
}
