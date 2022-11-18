using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TokenUnit : MonoBehaviour
{
    public UnitSO unit;

    // ========= Visual Componenets =========
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private GameObject armor;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private Image art;

    // ========= Token Functionality =========
    public int tHealth;
    public int tDamage;
    public int tArmor;

    public void SetUp(UnitSO givenUnit)
    {
        unit = givenUnit;

        // Visuals
        healthText.text = unit.health.ToString();
        armorText.text = unit.armor.ToString();
        damageText.text = unit.damage.ToString();

        art.sprite = unit.art;
        art.rectTransform.localPosition = unit.tokenArtOffset;
        var size = new Vector3(unit.tokenArtSize, unit.tokenArtSize, unit.tokenArtSize);
        art.rectTransform.localScale = size;

        // Stats
        tHealth = unit.health;
        tDamage = unit.damage;
        tArmor = unit.armor;
    }

    private void Update()
    {
        UpdateStats();
    }

    private void UpdateStats()
    {
        healthText.text = tHealth.ToString();
        armorText.text = tArmor.ToString();
        damageText.text = tDamage.ToString();

        if(tArmor > 0)
            armor.SetActive(true);
        else
            armor.SetActive(false);
    }

    // ========= Combat Functionality =========
    public void Attack()
    {
        Debug.Log(unit.title + " is attacking!");
    }

    public void TakeDamage(int dmg)
    {
        if(tArmor > 0)
        {
            tArmor -= dmg;
        }
        else
        {
            tHealth -= dmg;
        }

        if (tHealth <= 0) Die();
    }

    public void Die()
    {
        Debug.Log("Implement Death");
    }
}
