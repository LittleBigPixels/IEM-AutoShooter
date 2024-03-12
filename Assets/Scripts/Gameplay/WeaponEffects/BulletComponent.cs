using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    public Vector3 Velocity;

    public void Update()
    {
        transform.position += Time.deltaTime * Velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyComponent enemyComponent = other.gameObject.GetComponent<EnemyComponent>();
        if (enemyComponent != null)
        {
            enemyComponent.ApplyDamage(1);
            GameObject.Destroy(gameObject);
            
            
        }
    }
}