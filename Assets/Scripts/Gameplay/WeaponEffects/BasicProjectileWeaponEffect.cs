using System;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectileWeaponEffect : IWeaponEffect
{
    private readonly BulletComponent m_prefab;
    private readonly float m_speed;
    private readonly float m_rateOfFire;

    private float m_nextShotDelay = 0;

    public BasicProjectileWeaponEffect(BulletComponent prefab, float speed, float rateOfFire)
    {
        m_prefab = prefab;
        m_speed = speed;
        m_rateOfFire = rateOfFire;
    }

    public void UpdateAndShoot(Vector3 origin, Vector3 direction)
    {
        float delayBetweenShots = 1.0f / m_rateOfFire;
        m_nextShotDelay += Time.deltaTime;
        if (m_nextShotDelay > delayBetweenShots)
        {
            Shoot(origin, direction);
            m_nextShotDelay -= delayBetweenShots;
        }
    }

    private void Shoot(Vector3 origin, Vector3 direction)
    {
        BulletComponent bullet = GameObject.Instantiate<BulletComponent>(m_prefab);
        bullet.transform.position = origin;
        bullet.Velocity = direction * m_speed;
    }
}