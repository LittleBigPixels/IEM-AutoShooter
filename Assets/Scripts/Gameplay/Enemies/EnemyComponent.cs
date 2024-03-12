using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponent : BaseEnemyComponent
{
    public int DifficultyLevel = 1;
    
    public float Speed;
    
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
}