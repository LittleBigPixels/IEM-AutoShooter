using System;
using System.Collections.Generic;
using UnityEngine;

public class SpiralEnemyComponent : BaseEnemyComponent
{
    public int DifficultyLevel = 1;
    
    public float Speed = 12f;
    public float angleRotation = 60f;
    private PlayerComponent m_player;
    
    public void Start()
    {
        m_player = GameObject.FindObjectOfType<PlayerComponent>();
        
        // radius = GetDistance(m_player.transform, transform);
        // radiusSpeed = .05f;
        // theta = 1f;
        // thetaSpeed = .02f;
    }
    
    public void Update()
    {
    
        Vector3 direction = m_player.transform.position - transform.position;
        direction = new Vector3(direction.x, m_player.transform.position.y, direction.z);
        direction = Quaternion.Euler(0, angleRotation, 0) * direction;
        float distanceThisFrame = Speed * Time.deltaTime;
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }
    
    private float GetDistance(Transform a, Transform b)
    {
        return Mathf.Abs(Vector2.Distance(a.position, b.position));
    }
}