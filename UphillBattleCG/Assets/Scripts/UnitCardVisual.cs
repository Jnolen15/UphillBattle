using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitCardVisual : MonoBehaviour
{
    public UnitSO unit;

    // ========= Visual Componenets =========
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private TextMeshProUGUI position;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Image art;

    public void SetUp(UnitSO data)
    {
        unit = data;

        cost.text = unit.cost.ToString();
        health.text = unit.health.ToString();
        damage.text = unit.attack.ToString();
        position.text = unit.position.ToString();
        title.text = unit.title;
        description.text = unit.description;

        art.sprite = unit.art;
        art.rectTransform.localPosition = unit.cardArtOffset;
        var size = new Vector3(unit.cardArtSize, unit.cardArtSize, unit.cardArtSize);
        art.rectTransform.localScale = size;
    }
}
