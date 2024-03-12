using System;
using System.Collections.Generic;
using UnityEngine;

public class FireballComponent : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    [SerializeField]
    private int PassThrough = 5;

    public Vector3 Direction
    {
        get => Direction;
        set => Direction = value.normalized;
    }
    
    public void Update()
    {
        transform.position += Time.deltaTime * Speed * Direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyComponent enemyComponent = other.gameObject.GetComponent<EnemyComponent>();
        if (enemyComponent != null)
        {
            enemyComponent.ApplyDamage(1);
            PassThrough--;

            if (PassThrough <= 0)
                Destroy(gameObject);
        }
    }
}