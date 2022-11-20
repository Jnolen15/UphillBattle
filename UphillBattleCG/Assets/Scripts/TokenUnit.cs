using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TokenUnit : MonoBehaviour
{
    public UnitSO unit;
    public TokenSlot tokenSlot;
    public BoardManager boardManager;

    // ========= Visual Componenets =========
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private GameObject armor;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private Image art;

    // ========= Token Functionality =========
    public bool isEnemy;
    public int tHealth;
    public int tDamage;
    public int tArmor;

    public void SetUp(UnitSO givenUnit)
    {
        unit = givenUnit;
        tokenSlot = this.GetComponentInParent<TokenSlot>();
        boardManager = this.GetComponentInParent<BoardManager>();

        // Visuals
        healthText.text = unit.health.ToString();
        armorText.text = unit.armor.ToString();
        damageText.text = unit.attack.ToString();

        art.sprite = unit.art;
        art.rectTransform.localPosition = unit.tokenArtOffset;
        var size = new Vector3(unit.tokenArtSize, unit.tokenArtSize, unit.tokenArtSize);
        art.rectTransform.localScale = size;

        // Stats
        tHealth = unit.health;
        tDamage = unit.attack;
        tArmor = unit.armor;

        // Lists
        foreach(UnitFunction func in unit.AttackList)
            AttackList.Add(func);

        foreach (UnitFunction func in unit.DamageList)
            DamageList.Add(func);
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

        if (tHealth < unit.health)
            healthText.color = Color.red;
        else
            healthText.color = Color.black;
    }

    // ========= Combat Functionality =========
    [SerializeField] private List<UnitFunction> AttackList = new List<UnitFunction>();
    [SerializeField] private List<UnitFunction> DamageList = new List<UnitFunction>();

    public void OnAttack()
    {
        if (AttackList.Count > 0)
        {
            foreach (UnitFunction function in AttackList)
            {
                function.Activate(this);
            }
        }
        else
        {
            //Debug.Log(unit.title + " has no Attack Functions");
        }
    }

    public void OnDamage()
    {
        if (DamageList.Count > 0)
        {
            foreach (UnitFunction function in DamageList)
            {
                function.Activate(this);
            }
        }
        else
        {
            //Debug.Log(unit.title + " has no Damage Functions");
        }
    }

    public void TakeDamage(int dmg)
    {
        // If say you wanted an effect where incoming damage was scaled
        // Make a public incomingDamage variable
        // Then modify that damage in the SO function
        OnDamage();

        if (tArmor > 0)
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
        tokenSlot.UnslotToken(gameObject);
        Destroy(gameObject);
    }
}
