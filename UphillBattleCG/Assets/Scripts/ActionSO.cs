using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardSO/Action")]
public class ActionSO : ScriptableObject
{
    public Sprite art;
    public Vector2 cardArtOffset;
    public float cardArtSize;

    public int cost;

    public string title;
    public string description;

    public enum Position
    {
        Anywhere,
        Friendly,
        Enemy,
        FFrontline,
        FBackline,
        EFrontline,
        EBackline
    }
    public Position position;
    public bool needTarget;

    public List<ActionFunction> FuncList = new List<ActionFunction>();

    public void OnPlay(GameObject target)
    {
        if (FuncList.Count > 0)
        {
            foreach (ActionFunction function in FuncList)
            {
                Debug.Log("Playing " + title + " on " + target);
                function.Activate(target);
            }
        }
    }
}
