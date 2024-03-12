using System;
using System.Collections.Generic;
using UnityEngine;

public class FireballComponent : MonoBehaviour
{
    [SerializeField] private int PassThrough = 5;
    [SerializeField] private int Speed = 25;
    public bool IsOwnerPlayer = true;
    public Vector3 Velocity;

    public void Start(){
        IsOwnerPlayer = false;
    }

    public void Update()
    {
        transform.position += Time.deltaTime * Velocity * Speed;
        Debug.DrawRay(transform.position, Velocity, Color.red);
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