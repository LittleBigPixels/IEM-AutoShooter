using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WNEnemyComponent : BaseEnemyComponent
{
    public float RotationSpeed;

    public float Speed;

    private PlayerComponent m_player;

    public void Start()
    {
        m_player = GameObject.FindObjectOfType<PlayerComponent>();
    }

    public void Update()
    {

        Vector3 directionToPlayer = m_player.transform.position - transform.position;

        // Rotate around the player
        float rotationAmount = RotationSpeed * Time.deltaTime;
        directionToPlayer = Quaternion.Euler(0, rotationAmount, 0) * directionToPlayer;

        
        if (Vector3.Distance(transform.position, m_player.transform.position) < 2f)
        {
            RotationSpeed += 5f * Time.deltaTime;
        }

        transform.position += Time.deltaTime * Speed * directionToPlayer;
        transform.LookAt(m_player.transform.position);
    }

    public void SetDifficulty(int level)
    {
        Speed = level * 5f;
        RotationSpeed = level * 10f;
    }
}