using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardSO/Unit")]
public class UnitSO : ScriptableObject
{
    public int cost;
    public int health;
    public int damage;
    public int armor;

    public string title;
    public string description;

    public enum Position
    {
        Frontline,
        Backline,
        Versatile
    } 
    public Position position;

    public Sprite art;
    public Vector2 cardArtOffset;
    public float cardArtSize;
    public Vector2 tokenArtOffset;
    public float tokenArtSize;
}
