using System;
using System.Collections.Generic;
using UnityEngine;

public class FireballComponent : MonoBehaviour
{
    [SerializeField] private int PassThrough = 5;
    [SerializeField] private int Speed = 25;
    public bool IsOwnerPlayer = true;
    public Vector3 Velocity;

    public float Lifetime = 5;
    private float m_startTime; 
    
    public void Start(){
        IsOwnerPlayer = false;
    }

    public void OnActivate()
    {
        m_startTime = Time.time;
    }

    public void Update()
    {
        transform.position += Time.deltaTime * Velocity * Speed;
        Debug.DrawRay(transform.position, Velocity, Color.red);

        if (Time.time - m_startTime > Lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyComponent enemyComponent = other.gameObject.GetComponent<EnemyComponent>();
        if (enemyComponent != null && IsOwnerPlayer)
        {
            enemyComponent.ApplyDamage(1);
            PassThrough--;

            if (PassThrough <= 0)
                Destroy(gameObject);
        }

        PlayerComponent playerComponent = other.gameObject.GetComponent<PlayerComponent>();
        if (playerComponent != null && !IsOwnerPlayer)
        {
            playerComponent.ApplyDamage(1);
            Destroy(gameObject);
        }
    }
}