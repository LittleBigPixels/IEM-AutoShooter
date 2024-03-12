using System;
using System.Collections.Generic;
using UnityEngine;

public class FireballComponent : BulletComponent
{
    [SerializeField]
    private int PassThrough = 5;

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