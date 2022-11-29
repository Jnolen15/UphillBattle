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
    public GameManager gameManager;
    public BoardManager boardManager;

    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        boardManager = gameManager.boardManager;
    }

    private void OnEnable()
    {
        // Prevents cards scale changing when played quickly
        this.transform.localScale = new Vector3(0.5f, 0.5f);
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
        if (gameManager.state == GameManager.State.Play)
        {
            indicator = Instantiate(Resources.Load<GameObject>("PlayIndicator"), transform.parent.parent.parent);
            playerManager.HoldingCard(this.gameObject);
            OnDrag();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (gameManager.state == GameManager.State.Play && indicator!=null)
            indicator.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (gameManager.state == GameManager.State.Play && indicator != null)
        {
            GameObject.Destroy(indicator);
            Play();
            boardManager.ClearHighlight();
        }
    }

    // ========== OVERRIDE CLASSES ==========
    public abstract void SetUp();

    public abstract void Play();

    public abstract void OnDrag();
}
