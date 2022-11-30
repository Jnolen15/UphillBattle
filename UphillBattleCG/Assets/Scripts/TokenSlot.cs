using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TokenSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Token Data
    public enum Type
    {
        Friendly,
        Enemy
    }
    public Type type;

    public enum Position
    {
        Frontline,
        Backline
    }
    public Position position;

    public GameObject slotedToken;
    private TokenUnit tu;
    private UnitControl uc;
    private PlayerManager playerManager;
    private BoardManager BoardManager;

    private void Awake()
    {
        uc = this.GetComponent<UnitControl>();
        playerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>();
        BoardManager = GetComponentInParent<BoardManager>();
    }

    // ========= MOUSE INTERACTION =========
    public void OnPointerEnter(PointerEventData eventData)
    {
        playerManager.SetTokenSpace(this.gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        playerManager.SetTokenSpace(null);
    }

    // ========= TOKEN FUNCTION =========
    public bool CanTargetToken(bool needEmpty, bool targetAmbiguous, string ctype, string cpos)
    {
        // Token slot is empty or not
        if (!targetAmbiguous)
        {
            if (needEmpty && HasToken()) return false;
            if (!needEmpty && !HasToken()) return false;
        }

        // Friendly
        if (type == Type.Friendly)
        {
            if (ctype == "Enemy") return false;
        }
        // Enemy
        else if (type == Type.Enemy)
        {
            if (ctype == "Friendly") return false;
        }

        // Frontline
        if (position == Position.Frontline)
        {
            if (cpos == "Backline") return false;
        }
        // Backline
        else if (position == Position.Backline)
        {
            if (cpos == "Frontline") return false;
        }

        return true;
    }

    public void SlotToken(GameObject token)
    {
        slotedToken = token;
        slotedToken.transform.position = this.transform.position;
        AnimatePlacement();
        tu = token.GetComponent<TokenUnit>();
        uc.SetUpUnit(tu);
        BoardManager.TokenSloted(gameObject);
    }

    public void UnslotToken()
    {
        uc.RemoveUnit();
        slotedToken = null;
        BoardManager.TokenOpen(gameObject);
    }

    public bool HasToken()
    {
        if (slotedToken != null) return true;
        return false;
    }

    // MAY JUST WANT TO REPLACE THIS WITH AN ANIMATION
    public void AnimatePlacement()
    {
        //StopAllCoroutines();
        StartCoroutine(LerpPos());
    }
    public void AnimateHorizontal(bool reverse)
    {
        //StopAllCoroutines();
        StartCoroutine(LerpHorizontal(reverse));
    }


    IEnumerator LerpPos()
    {
        float time = 0;
        float duration = 0.15f;
        Vector3 endPos = transform.localPosition;
        Vector3 endScale = transform.localScale;
        Vector3 startPos = new Vector3(transform.localPosition.x, transform.localPosition.y+100, transform.localPosition.z -50);
        Vector3 startScale = new Vector3(transform.localScale.x + 0.1f, transform.localScale.y + 0.1f, transform.localScale.z);

        while (time < duration)
        {
            transform.localPosition = Vector3.Lerp(startPos, endPos, time / duration);
            transform.localScale = Vector3.Lerp(startScale, endScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = endPos;
    }

    IEnumerator LerpHorizontal(bool reverse)
    {
        float time = 0;
        float duration = 0.3f;
        Vector3 startPos = transform.localPosition;
        Vector3 endPos = new Vector3(transform.localPosition.x, transform.localPosition.y + 60, transform.localPosition.z);
        if(reverse)
            endPos = new Vector3(transform.localPosition.x, transform.localPosition.y - 60, transform.localPosition.z);

        while (time < duration)
        {
            if(time < duration / 2)
            {
                float t = time / (duration / 2);
                t = t * t * (3f - 2f * t);
                transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            } else
            {
                float t = time / duration;
                t = t * t * (3f - 2f * t);
                transform.localPosition = Vector3.Lerp(endPos, startPos, t);
            }

            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = startPos;
    }
}
