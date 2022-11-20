using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI courageText;

    public GameObject tokenPrefab;
    public bool holdingCard;
    public GameObject curCard;
    public GameObject tokenSpace;
    [SerializeField] private int gold;
    [SerializeField] private int courage;

    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            goldText.text = "Gold: " + gold.ToString();
        }
    }
    public int Courage
    {
        get { return courage; }
        set
        {
            courage = value;
            courageText.text = "Courage: " + courage.ToString();
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
        var canPlay = ts.CanTargetToken(true, "Friendly", unitData.position.ToString());
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
                canPlay = ts.CanTargetToken(actionData.needTarget, "Anywhere", "Versatile");
                break;
            case ActionSO.Position.Friendly:
                canPlay = ts.CanTargetToken(actionData.needTarget, "Friendly", "Versatile");
                break;
            case ActionSO.Position.FFrontline:
                canPlay = ts.CanTargetToken(actionData.needTarget, "Friendly", "Frontline");
                break;
            case ActionSO.Position.FBackline:
                canPlay = ts.CanTargetToken(actionData.needTarget, "Friendly", "Backline");
                break;
            case ActionSO.Position.Enemy:
                canPlay = ts.CanTargetToken(actionData.needTarget, "Enemy", "Versatile");
                break;
            case ActionSO.Position.EFrontline:
                canPlay = ts.CanTargetToken(actionData.needTarget, "Enemy", "Frontline");
                break;
            case ActionSO.Position.EBackline:
                canPlay = ts.CanTargetToken(actionData.needTarget, "Enemy", "Backline");
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
