using UnityEngine;

namespace WeaponEffects
{
    public class ArcProjectileWeaponEffect : IWeaponEffect
    {
        private readonly BulletComponent m_prefab;
        private readonly float m_speed;
        private readonly float m_rateOfFire;
        private readonly float m_numberOfBullets;
        private readonly int m_radius;

        private float m_nextShotDelay = 0;

        public ArcProjectileWeaponEffect(BulletComponent prefab, int numberOfBullets, float speed, float rateOfFire, int arcAngle)
        {
            m_prefab = prefab;
            m_speed = speed;
            m_rateOfFire = rateOfFire;
            m_numberOfBullets = numberOfBullets;
            m_radius = arcAngle;
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
            float angle = m_radius / m_numberOfBullets;
            
            direction = Rotate(direction, -(m_radius/2));
                
            for (int i = 0; i < m_numberOfBullets; i++)
            {
                BulletComponent bullet = GameObject.Instantiate<BulletComponent>(m_prefab);
                bullet.transform.position = origin;

                direction = Rotate(direction, angle);
                
                bullet.Velocity = direction * m_speed;
            }
        }

        public Vector3 Rotate(Vector3 v, float angleDregee)
        {
            Quaternion q = Quaternion.AngleAxis(angleDregee, Vector3.up);
            return q * v;
        }
    }
}