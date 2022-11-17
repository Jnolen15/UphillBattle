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
    public void CardPlayed(GameObject card)
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
        if (tokenSpace!= null)
        {
            var token = Instantiate<GameObject>(tokenPrefab, tokenSpace.transform);
            token.GetComponent<TokenUnit>().SetUp(unitData);
            tokenSpace.GetComponent<TokenSlot>().SlotToken(token);
            return true;
        }
        return false;
    }
}
