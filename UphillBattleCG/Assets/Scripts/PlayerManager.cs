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
        var canPlay = ts.CanTargetToken("Friendly", unitData.position.ToString());
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
}
