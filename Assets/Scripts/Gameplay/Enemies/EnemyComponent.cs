using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponent : MonoBehaviour
{
    public int DifficultyLevel = 1;
    
    public float Speed;
    public int Health;
    
    private PlayerComponent m_player;
    
    public void Start()
    {
        m_player = GameObject.FindObjectOfType<PlayerComponent>();
    }
    
    public void Update()
    {
        Vector3 directionToPlayer = m_player.transform.position - transform.position;
        directionToPlayer = directionToPlayer.normalized;

        transform.position += Time.deltaTime * Speed * directionToPlayer;
    }

    public void ApplyDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
}