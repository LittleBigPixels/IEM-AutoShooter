using System;
using System.Collections.Generic;
using UnityEngine;

public class SweepWeaponEffect : IWeaponEffect
{
    private readonly BulletComponent m_prefab;
    private readonly Gradient m_colors;
    private readonly float m_bulletSpeed;
    private readonly float m_rateOfFire;
    private readonly float m_rotationSpeed;

    private float m_nextShotDelay = 0;

    public SweepWeaponEffect(BulletComponent prefab, Gradient colors, float bulletSpeed, float rateOfFire,
        float rotationSpeed)
    {
        m_prefab = prefab;
        m_colors = colors;
        m_bulletSpeed = bulletSpeed;
        m_rateOfFire = rateOfFire;
        m_rotationSpeed = rotationSpeed;
    }

    public void UpdateAndShoot(Vector3 origin, Vector3 direction)
    {
        float delayBetweenShots = 1.0f / m_rateOfFire;
        m_nextShotDelay += Time.deltaTime;
        if (m_nextShotDelay > delayBetweenShots)
        {
            float angle = Time.time * m_rotationSpeed;
            //Vector3 shootDirection = Mathf.Cos(angle) * Vector3.forward + Mathf.Sin(angle) * Vector3.left;
            Vector3 shootDirection = Rotate(Vector3.forward, angle);
            
            BulletComponent bullet = GameObject.Instantiate<BulletComponent>(m_prefab);
            bullet.transform.position = origin;
            bullet.Velocity = shootDirection * m_bulletSpeed;
            bullet.transform.localScale = 2 * bullet.transform.localScale;

            Color color = m_colors.Evaluate((angle % Mathf.PI * 2) / (Mathf.PI * 2));
            var bulletMeshRenderer = bullet.GetComponent<MeshRenderer>();
            bulletMeshRenderer.material.color = color;

            m_nextShotDelay -= delayBetweenShots;
        }
    }

    public Vector3 Rotate(Vector3 v, float angleRadians)
    {
        Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angleRadians, Vector3.up);
        return q * v;
    }
}