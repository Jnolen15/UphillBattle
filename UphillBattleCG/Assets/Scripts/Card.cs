using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Card : MonoBehaviour, 
    IPointerEnterHandler,
    IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer entered " + gameObject.name);
        this.transform.localScale = this.transform.localScale * 1.5f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer exited " + gameObject.name);
        this.transform.localScale = this.transform.localScale / 1.5f;
    }

    public abstract void SetUp();
}
