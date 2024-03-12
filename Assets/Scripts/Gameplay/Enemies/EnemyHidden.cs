using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHidden : BaseEnemyComponent
{
    public int DifficultyLevel = 1;

    public float Speed;

    public float distanceBeforeStop = 5f;

    private PlayerComponent m_player;

    public void Start()
    {
        m_player = GameObject.FindObjectOfType<PlayerComponent>();
    }

    public void Update()
    {
        Vector3 directionToPlayer = m_player.transform.position - transform.position;


        float distanceFromPlayer = Mathf.Sqrt(Mathf.Pow(m_player.transform.position.x - transform.position.x, 2) + Mathf.Pow(m_player.transform.position.y - transform.position.y, 2) + Mathf.Pow(m_player.transform.position.z - transform.position.z, 2));
        if(distanceFromPlayer > distanceBeforeStop)
        {
            directionToPlayer = directionToPlayer.normalized;

            transform.position += Time.deltaTime * Speed * directionToPlayer;
        }


    }
}
