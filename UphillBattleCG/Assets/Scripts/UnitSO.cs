using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardSO/Unit")]
public class UnitSO : ScriptableObject
{
    public Sprite art;
    public Vector2 cardArtOffset;
    public float cardArtSize;
    public Vector2 tokenArtOffset;
    public float tokenArtSize;

    public int cost;
    public int health;
    public int attack;
    public int armor;

    public string title;
    public string description;

    public bool hasAbility;

    public enum Position
    {
        Frontline,
        Backline,
        Versatile
    } 
    public Position position;

    public List<UnitFunction> AttackList = new List<UnitFunction>();
    public List<UnitFunction> DamageList = new List<UnitFunction>();
    public List<UnitFunction> PlayList = new List<UnitFunction>();
    public List<UnitFunction> OnTurnList = new List<UnitFunction>();
    public List<UnitFunction> DeathList = new List<UnitFunction>();
}
