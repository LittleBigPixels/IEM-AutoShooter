using System;
using System.Collections.Generic;
using UnityEngine;

public class WeirdEnemyComponent : BaseEnemyComponent
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
        float distanceToPlayer = Vector3.Distance(m_player.transform.position,transform.position);
        transform.LookAt(m_player.transform.position);
        transform.rotation *= Quaternion.AngleAxis(1080.0f/distanceToPlayer, Vector3.up);
        transform.position += Time.deltaTime * Speed * transform.forward;
    }
}