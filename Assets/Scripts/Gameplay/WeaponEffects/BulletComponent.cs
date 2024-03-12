using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    public bool IsOwnerPlayer = true;
    public Vector3 Velocity;
    public float MaxDuration = 5f;
    public float SpawnTime;

    private void Start() { }

    public void Update()
    {
        if (SpawnTime == 0)
            SpawnTime = Time.time;
        
        if (Time.time > SpawnTime + MaxDuration)
        {
            Destroy(gameObject);
        }

        transform.position += Time.deltaTime * Velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        BaseEnemyComponent enemyComponent = other.gameObject.GetComponent<BaseEnemyComponent>();
        if (enemyComponent != null && IsOwnerPlayer)
        {
            enemyComponent.ApplyDamage(1);
            GameObject.Destroy(gameObject);
        }

        PlayerComponent playerComponent = other.gameObject.GetComponent<PlayerComponent>();
        if (playerComponent != null && !IsOwnerPlayer)
        {
            playerComponent.ApplyDamage(1);
            GameObject.Destroy(gameObject);
        }
    }
}