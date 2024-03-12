using System;
using System.Collections.Generic;
using UnityEngine;

public class ParallelProjectileWeaponEffect : IWeaponEffect
{
    public const float Offset = 0.45f;

    private readonly BulletComponent m_prefab;
    private readonly int m_multiShotCount;
    private readonly float m_speed;
    private readonly float m_rateOfFire;

    private float m_nextShotDelay = 0;

    public ParallelProjectileWeaponEffect(BulletComponent prefab, int multiShotCount, float speed, float rateOfFire)
    {
        m_prefab = prefab;
        m_multiShotCount = multiShotCount;
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
        Vector3 sideDirection = Vector3.Cross(Vector3.up, direction);
        float minOffset = Offset * m_multiShotCount / 2;
        float maxOffset = -minOffset;

        for (int i = 0; i < m_multiShotCount; i++)
        {
            float offset = 0;
            if (m_multiShotCount > 1)
                offset = minOffset + (maxOffset - minOffset) * i / (float)(m_multiShotCount - 1);

            BulletComponent bullet = GameObject.Instantiate<BulletComponent>(m_prefab);
            bullet.transform.position = origin + offset * sideDirection;
            bullet.Velocity = direction * m_speed;
        }
    }
}