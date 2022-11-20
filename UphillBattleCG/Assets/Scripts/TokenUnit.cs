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
    [SerializeField] private int tHealth;
    public int THealth
    {
        get { return tHealth; }
        set
        {
            tHealth = value;
            UpdateStats();
        }
    }
    [SerializeField] private int tDamage;
    public int TDamage
    {
        get { return tDamage; }
        set
        {
            tDamage = value;
            UpdateStats();
        }
    }
    [SerializeField] private int tArmor;
    public int TArmor
    {
        get { return tArmor; }
        set
        {
            tArmor = value;
            if (tArmor < 0) tArmor = 0;
            UpdateStats();
        }
    }

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
        THealth = unit.health;
        TDamage = unit.attack;
        TArmor = unit.armor;

        // Lists
        foreach(UnitFunction func in unit.AttackList)
            AttackList.Add(func);

        foreach (UnitFunction func in unit.DamageList)
            DamageList.Add(func);

        foreach (UnitFunction func in unit.PlayList)
            PlayList.Add(func);

        // On Play
        OnPlay();
    }

    private void UpdateStats()
    {
        healthText.text = THealth.ToString();
        armorText.text = TArmor.ToString();
        damageText.text = TDamage.ToString();

        if(TArmor > 0)
            armor.SetActive(true);
        else
            armor.SetActive(false);

        if (THealth < unit.health)
            healthText.color = Color.red;
        else
            healthText.color = Color.black;
    }

    // ========= Combat Functionality =========
    public void TakeDamage(int dmg)
    {
        // If say you wanted an effect where incoming damage was scaled
        // Make a public incomingDamage variable
        // Then modify that damage in the SO function
        OnDamage();

        if (TArmor > 0)
        {
            TArmor -= dmg;
        }
        else
        {
            THealth -= dmg;
        }

        if (THealth <= 0) Die();
    }

    public void Die()
    {
        Debug.Log("Implement Death");
        tokenSlot.UnslotToken(gameObject);
        Destroy(gameObject);
    }

    // ========= Evenet Triggers =========
    [SerializeField] private List<UnitFunction> AttackList = new List<UnitFunction>();
    [SerializeField] private List<UnitFunction> DamageList = new List<UnitFunction>();
    [SerializeField] private List<UnitFunction> PlayList = new List<UnitFunction>();

    public void OnPlay()
    {
        if (AttackList.Count > 0)
        {
            foreach (UnitFunction function in PlayList)
            {
                function.Activate(this);
            }
        }
    }

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

    
}