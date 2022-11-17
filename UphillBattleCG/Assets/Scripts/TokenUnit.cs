using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TokenUnit : MonoBehaviour
{
    public UnitSO unit;

    // ========= Visual Componenets =========
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI armor;
    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private Image art;

    public void SetUp(UnitSO givenUnit)
    {
        unit = givenUnit;

        health.text = unit.health.ToString();
        armor.text = unit.armor.ToString();
        damage.text = unit.damage.ToString();

        art.sprite = unit.art;
        art.rectTransform.localPosition = unit.tokenArtOffset;
        var size = new Vector3(unit.tokenArtSize, unit.tokenArtSize, unit.tokenArtSize);
        art.rectTransform.localScale = size;
    }
}
