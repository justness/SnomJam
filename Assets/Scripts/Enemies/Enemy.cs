using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy: MonoBehaviour
{
    public int health;
    public int attack;
    public Vector3 startPos;

    public abstract void Attack();
    public abstract void Follow();
    public abstract void Reset();
}
