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
        AnimatePlacement();
    }

    // MAY JUST WANT TO REPLACE THIS WITH AN ANIMATION
    public void AnimatePlacement()
    {
        StopAllCoroutines();
        StartCoroutine(LerpPos());
    }

    IEnumerator LerpPos()
    {
        float time = 0;
        float duration = 0.15f;
        Vector3 startPos = new Vector3(transform.localPosition.x, transform.localPosition.y+100, transform.localPosition.z -50);
        Vector3 startScale = new Vector3(transform.localScale.x + 0.1f, transform.localScale.y + 0.1f, transform.localScale.z);
        Vector3 endPos = transform.localPosition;
        Vector3 endScale = transform.localScale;

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
