using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public List<GameObject> TokenSlots = new List<GameObject>();
    public List<GameObject> OpenSlots = new List<GameObject>();
    public List<GameObject> OpenEnemySlots = new List<GameObject>();

    public int rows;
    public int columns;

    public void TokenSloted(GameObject token)
    {
        if (OpenSlots.Contains(token))
            OpenSlots.Remove(token);
        else
            Debug.LogError("Not in open slot list, can't remove");

        if (token.GetComponent<TokenSlot>().type == TokenSlot.Type.Enemy)
        {
            if (OpenEnemySlots.Contains(token))
                OpenEnemySlots.Remove(token);
            else
                Debug.LogError("Not in open Enemy slot list, can't remove");
        }
    }

    public void TokenOpen(GameObject token)
    {
        OpenSlots.Add(token);

        if (token.GetComponent<TokenSlot>().type == TokenSlot.Type.Enemy)
            OpenEnemySlots.Add(token);
    }

    public void PlayerCombat()
    {
        Debug.Log("Starting Player Combat");

        var start = (TokenSlots.Count / 2);
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

    public GameObject GetOpposingFrontline(TokenSlot ts)
    {
        var thisPos = TokenSlots.IndexOf(ts.gameObject);
        var dist = 0;
        GameObject slot = TokenSlots[thisPos];

        if (ts.position == TokenSlot.Position.Frontline)
            dist = 3;
        else if (ts.position == TokenSlot.Position.Backline)
            dist = 6;

        if (ts.type == TokenSlot.Type.Friendly)
            slot = TokenSlots[thisPos - dist];
        else if (ts.type == TokenSlot.Type.Enemy)
            slot = TokenSlots[thisPos + dist];

        if (slot.GetComponent<TokenSlot>().HasToken())
            return slot.GetComponent<TokenSlot>().slotedToken;

        return null;
    }

    public GameObject GetOpposingBackline(TokenSlot ts)
    {
        var thisPos = TokenSlots.IndexOf(ts.gameObject);
        var dist = 0;
        GameObject slot = TokenSlots[thisPos];

        if (ts.position == TokenSlot.Position.Frontline)
            dist = 6;
        else if (ts.position == TokenSlot.Position.Backline)
            dist = 9;

        if (ts.type == TokenSlot.Type.Friendly)
            slot = TokenSlots[thisPos - dist];
        else if (ts.type == TokenSlot.Type.Enemy)
            slot = TokenSlots[thisPos + dist];

        if (slot.GetComponent<TokenSlot>().HasToken())
            return slot.GetComponent<TokenSlot>().slotedToken;

        return null;
    }

    public GameObject GetRandomOpenEnemySlot()
    {
        if (OpenEnemySlots.Count <= 0) return null;

        var rand = Random.Range(0, OpenEnemySlots.Count);
        return OpenEnemySlots[rand];
    }
}