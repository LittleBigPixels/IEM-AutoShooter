using System;
using System.Collections.Generic;
using UnityEngine;



public class ArcProjectilweaponEffect :IWeaponEffect
{
    public const float Offset = 0.45f;

    private readonly BulletComponent m_prefab;
    private readonly int m_multiShotCount;
    private readonly float m_speed;
    private readonly float m_rateOfFire;

    private float m_nextShotDelay = 0;

    public ArcProjectilweaponEffect(BulletComponent prefab, int multiShotCount, float speed, float rateOfFire)
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
            Shoot(origin, direction,90.0f);
            m_nextShotDelay -= delayBetweenShots;
        }
    }

    private void Shoot(Vector3 origin, Vector3 direction, float angle)
    {
        float distanceBetweenOffSet = angle / (m_multiShotCount - 1);

        for (int i = 0; i < m_multiShotCount; i++)
        {
            float offset = 0;
            if (m_multiShotCount > 1)
                offset = distanceBetweenOffSet * i - angle/2;
            
            Debug.Log("pre-direction");
            direction = Rotate(direction, offset);
            Debug.Log(direction);

            BulletComponent bullet = GameObject.Instantiate<BulletComponent>(m_prefab);
            bullet.transform.position = origin;
            bullet.Velocity = direction * m_speed;
        }
    }

    public Vector3 Rotate(Vector3 v, float angleRadiant)
    {
        Quaternion q = Quaternion.AngleAxis(angleRadiant, Vector3.up);
        return q * v;
    }
}