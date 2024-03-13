using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyComponent : MonoBehaviour
{
    public int Health;
    
    public void ApplyDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            GameObject.Destroy(gameObject);
            Game.Enemies.Remove(this);
        }
    }
}