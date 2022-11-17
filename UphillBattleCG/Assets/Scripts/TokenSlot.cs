using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TokenSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject slotedToken;

    private PlayerManager playerManager;
    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        playerManager.SetTokenSpace(this.gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        playerManager.SetTokenSpace(null);
    }

    public void SlotToken(GameObject token)
    {
        slotedToken = token;
        slotedToken.transform.position = this.transform.position;
        //slotedToken.transform.SetParent(this.transform);
    }
}
