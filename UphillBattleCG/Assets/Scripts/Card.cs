using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Card : MonoBehaviour, 
    IPointerEnterHandler,
    IPointerExitHandler,
    IDragHandler,
    IBeginDragHandler,
    IEndDragHandler
{

    private GameObject indicator;
    public PlayerManager playerManager;

    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>();
    }

    // ========== MOUSE CONTROLS ==========
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.parent.GetComponent<HandSlot>().HighlightSlot(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.parent.GetComponent<HandSlot>().HighlightSlot(false);
    }

    // ========== DRAG DROP ==========
    public void OnBeginDrag(PointerEventData eventData)
    {
        indicator = Instantiate(Resources.Load<GameObject>("PlayIndicator"), transform.parent.parent.parent);
        playerManager.HoldingCard(this.gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        indicator.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject.Destroy(indicator);
        Play();
    }

    // ========== OVERRIDE CLASSES ==========
    public abstract void SetUp();

    public abstract void Play();
}
