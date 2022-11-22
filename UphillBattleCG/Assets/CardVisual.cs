using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardVisual : MonoBehaviour
{
    public ActionSO action;

    // ========= Visual Componenets =========
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Image art;

    public void SetUp(ActionSO data)
    {
        action = data;
        cost.text = action.cost.ToString();
        title.text = action.title;
        description.text = action.description;

        art.sprite = action.art;
        art.rectTransform.localPosition = action.cardArtOffset;
        var size = new Vector3(action.cardArtSize, action.cardArtSize, action.cardArtSize);
        art.rectTransform.localScale = size;
    }
}
