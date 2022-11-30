using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class TokenUnit : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler
{
    public UnitSO unit;
    public TokenSlot tokenSlot;
    public BoardManager boardManager;
    public PlayerManager playerManager;
    public GameManager gameManager;

    // ========= Visual Componenets =========
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private GameObject armor;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private GameObject moreInfo;
    [SerializeField] private Image art;

    // ========== MOUSE CONTROLS ==========
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Hovered over " + unit.title);
        gameManager.PreviewCard(unit);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Hovered off " + unit.title);
        gameManager.ClosePreview();
    }

    // ========= Token Functionality =========
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
        playerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

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

        if (unit.hasAbility)
            moreInfo.SetActive(true);
        else
            moreInfo.SetActive(false);

        // Lists
        foreach (UnitFunction func in unit.AttackList)
            AttackList.Add(func);

        foreach (UnitFunction func in unit.DamageList)
            DamageList.Add(func);

        foreach (UnitFunction func in unit.PlayList)
            PlayList.Add(func);
        
        foreach (UnitFunction func in unit.DeathList)
            DeathList.Add(func);
        
        foreach (UnitFunction func in unit.OnTurnList)
            OnTurnList.Add(func);

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
        if (TArmor > 0)
        {
            TArmor -= dmg;
        }
        else
        {
            THealth -= dmg;
        }

        if (THealth <= 0) Die();

        OnDamage();
    }

    public void Die()
    {
        Debug.Log(unit.title + " Dies");
        gameManager.ClosePreview();
        OnDeath();
        tokenSlot.UnslotToken();
        Destroy(gameObject);
    }

    // ========= Evenet Triggers =========
    [SerializeField] private List<UnitFunction> AttackList = new List<UnitFunction>();
    [SerializeField] private List<UnitFunction> DamageList = new List<UnitFunction>();
    [SerializeField] private List<UnitFunction> PlayList = new List<UnitFunction>();
    [SerializeField] private List<UnitFunction> OnTurnList = new List<UnitFunction>();
    [SerializeField] private List<UnitFunction> DeathList = new List<UnitFunction>();

    public void OnPlay()
    {
        if (PlayList.Count > 0)
        {
            foreach (UnitFunction function in PlayList)
            {
                function.Activate(this);
            }
        }
    }

    public void OnTurn()
    {
        if (OnTurnList.Count > 0)
        {
            foreach (UnitFunction function in OnTurnList)
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
    }

    public void OnDeath()
    {
        if (DeathList.Count > 0)
        {
            foreach (UnitFunction function in DeathList)
            {
                function.Activate(this);
            }
        }
    }
}
