using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public List<GameObject> TokenSlots = new List<GameObject>();

    public int rows;
    public int columns;

    public void PlayerCombat()
    {
        Debug.Log("Starting Player Combat");

        var start = (TokenSlots.Count / 2) - 1;
        for (int i = start; i < TokenSlots.Count; i++)
        {
            if (TokenSlots[i].GetComponent<TokenSlot>().HasToken())
            {
                TokenSlots[i].GetComponent<UnitControl>().AttackToken();
            }
        }
    }

    public void EnemyCombat()
    {
        Debug.Log("Starting Enemy Combat");

        var end = (TokenSlots.Count / 2);
        for (int i = 0; i < end; i++)
        {
            if (TokenSlots[i].GetComponent<TokenSlot>().HasToken())
            {
                TokenSlots[i].GetComponent<UnitControl>().AttackToken();
            }
        }
    }
}
