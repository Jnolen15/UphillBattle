using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI courageText;
    [SerializeField] private TextMeshProUGUI healthText;
    //[SerializeField] private TextMeshProUGUI killsText;
    //[SerializeField] private TextMeshProUGUI deathsText;

    public GameObject tokenPrefab;
    public bool holdingCard;
    public GameObject curCard;
    public GameObject tokenSpace;
    [SerializeField] private int gold;
    [SerializeField] private int courage;
    [SerializeField] private int health;
    [SerializeField] private int kills;
    [SerializeField] private int deaths;

    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            goldText.text = gold.ToString();
        }
    }
    public int Courage
    {
        get { return courage; }
        set
        {
            courage = value;
            if (courage > 10)
                courage = 10;
            if (courage < -5)
                courage = -5;
            courageText.text = courage.ToString();
        }
    }
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            healthText.text = health.ToString();
        }
    }
    public int Kills
    {
        get { return kills; }
        set
        {
            kills = value;
            //killsText.text = "Kills: " + kills.ToString();
        }
    }
    public int Deaths
    {
        get { return deaths; }
        set
        {
            deaths = value;
            //deathsText.text = "Deaths: " + deaths.ToString();
        }
    }

    // MAY NOT NEED
    public void HoldingCard(GameObject card)
    {
        holdingCard = true;
        curCard = card;
    }
    // MAY NOT NEED
    public void CardPlayed()
    {
        holdingCard = false;
        curCard = null;
    }

    public void SetTokenSpace(GameObject slot)
    {
        tokenSpace = slot;
    }

    public bool TryPlayUnitCard(UnitSO unitData)
    {
        // Hovering over a token slot
        if (tokenSpace == null) return false;

        // Correct position
        var ts = tokenSpace.GetComponent<TokenSlot>();
        var canPlay = ts.CanTargetToken(true, false, "Friendly", unitData.position.ToString());
        if (!canPlay) return false;

        // Can afford it
        if (Gold < unitData.cost) return false;
        else Gold -= unitData.cost;

        // Play token
        var token = Instantiate<GameObject>(tokenPrefab, tokenSpace.transform);
        token.GetComponent<TokenUnit>().SetUp(unitData);
        tokenSpace.GetComponent<TokenSlot>().SlotToken(token);

        return true;
    }

    public bool TryPlayActionCard(ActionSO actionData)
    {
        // Hovering over a token slot
        if (tokenSpace == null) return false;

        // Correct position
        var ts = tokenSpace.GetComponent<TokenSlot>();
        var canPlay = false;

        switch (actionData.position)
        {
            default:
                break;
            case ActionSO.Position.Anywhere:
                canPlay = ts.CanTargetToken(actionData.needEmpty, actionData.targetAmbiguous, "Anywhere", "Versatile");
                break;
            case ActionSO.Position.Friendly:
                canPlay = ts.CanTargetToken(actionData.needEmpty, actionData.targetAmbiguous, "Friendly", "Versatile");
                break;
            case ActionSO.Position.FFrontline:
                canPlay = ts.CanTargetToken(actionData.needEmpty, actionData.targetAmbiguous, "Friendly", "Frontline");
                break;
            case ActionSO.Position.FBackline:
                canPlay = ts.CanTargetToken(actionData.needEmpty, actionData.targetAmbiguous, "Friendly", "Backline");
                break;
            case ActionSO.Position.Enemy:
                canPlay = ts.CanTargetToken(actionData.needEmpty, actionData.targetAmbiguous, "Enemy", "Versatile");
                break;
            case ActionSO.Position.EFrontline:
                canPlay = ts.CanTargetToken(actionData.needEmpty, actionData.targetAmbiguous, "Enemy", "Frontline");
                break;
            case ActionSO.Position.EBackline:
                canPlay = ts.CanTargetToken(actionData.needEmpty, actionData.targetAmbiguous, "Enemy", "Backline");
                break;
        }
        
        if (!canPlay) return false;

        // Can afford it
        if (Gold < actionData.cost) return false;
        else Gold -= actionData.cost;

        // Play action
        actionData.OnPlay(tokenSpace);

        return true;
    }
}
