using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitControl : MonoBehaviour
{
    public enum Type
    {
        friendly,
        enemy
    }
    public Type type;

    public enum Position
    {
        frontline,
        backline
    }
    public Position position;
}
