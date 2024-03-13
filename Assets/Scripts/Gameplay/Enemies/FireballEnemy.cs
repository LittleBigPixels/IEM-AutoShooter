using System;
using System.Collections.Generic;
using UnityEngine;

public class FireballEnemy : BaseEnemyComponent
{
    public float Speed;
    public float FireballCooldown;
    public GameObject FireballPrefab;

    private PlayerComponent m_player;
    
    private float m_fireballCooldownTimer;
    
    public void Start()
    {
        m_fireballCooldownTimer = 0.0f;
        m_player = GameObject.FindObjectOfType<PlayerComponent>();
    }
    
    public void Update()
    {
        // Stay 5m away from the player
        Vector3 directionToPlayer = m_player.transform.position - transform.position;
        Vector3 directionToDestination = directionToPlayer - 5.0f * directionToPlayer.normalized;
        Vector3 movement = directionToDestination.normalized;

        movement.y = 0.0f;
        transform.position += Time.deltaTime * Speed * movement;

        m_fireballCooldownTimer -= Time.deltaTime;
        if (m_fireballCooldownTimer <= 0.0f)
        {
            m_fireballCooldownTimer = FireballCooldown;
            GameObject fireball = GameObject.Instantiate(FireballPrefab);
            fireball.transform.position = transform.position;
            FireballComponent fireballComponent = fireball.GetComponent<FireballComponent>();
            fireballComponent.Velocity = directionToPlayer.normalized;
            fireballComponent.OnActivate();
        }
    }
}