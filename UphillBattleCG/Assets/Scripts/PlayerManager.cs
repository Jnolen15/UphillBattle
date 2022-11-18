using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject tokenPrefab;
    public bool holdingCard;
    public GameObject curCard;
    public GameObject tokenSpace;

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
        if (tokenSpace == null) return false;
        var uc = tokenSpace.GetComponent<UnitControl>();
        if (uc.type != UnitControl.Type.friendly) return false;

        // Frontline
        if(uc.position == UnitControl.Position.frontline)
        {
            if(unitData.position == UnitSO.Position.Backline) return false;
        }
        // Backline
        else if (uc.position == UnitControl.Position.backline)
        {
            if (unitData.position == UnitSO.Position.Frontline) return false;
        }

        var token = Instantiate<GameObject>(tokenPrefab, tokenSpace.transform);
        token.GetComponent<TokenUnit>().SetUp(unitData);
        tokenSpace.GetComponent<TokenSlot>().SlotToken(token);

        return true;
    }
}
