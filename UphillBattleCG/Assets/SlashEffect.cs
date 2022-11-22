using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlashEffect : MonoBehaviour
{
    public void Animate(bool isEnemy)
    {
        transform.rotation = Quaternion.Euler(20, transform.rotation.y, 90);
        if(isEnemy)
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        StartCoroutine(LerpPos(isEnemy));
    }

    IEnumerator LerpPos(bool isEnemy)
    {
        float time = 0;
        float duration = 0.2f;
        Vector3 startPos = transform.localPosition;
        Vector3 endPos = new Vector2(transform.localPosition.x, transform.localPosition.y + 20);
        if (isEnemy)
            endPos = new Vector2(transform.localPosition.x, transform.localPosition.y - 20);
        float startAlpha = GetComponent<Image>().color.a;
        float endAlpha = 0.2f;

        while (time < duration)
        {
            transform.localPosition = Vector3.Lerp(startPos, endPos, time / duration);
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, newAlpha);
            time += Time.deltaTime;
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
