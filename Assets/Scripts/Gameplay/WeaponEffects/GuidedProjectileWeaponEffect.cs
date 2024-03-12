using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class GuidedProjectileWeaponEffect : IWeaponEffect
{
    private readonly BulletComponent m_prefab;
    private readonly float m_speed;
    private readonly float m_rateOfFire;

    private float m_nextShotDelay = 0;

    public GuidedProjectileWeaponEffect(BulletComponent prefab, float speed, float rateOfFire)
    {
        m_prefab = prefab;
        m_speed = speed;
        m_rateOfFire = rateOfFire;
    }


    private void Shoot(Vector3 origin, Vector3 direction)
    {
        BulletComponent bullet = GameObject.Instantiate<BulletComponent>(m_prefab);
        bullet.transform.position = origin;
        bullet.Velocity = direction * m_speed;
    }
    
    public void UpdateAndShoot(Vector3 origin, Vector3 direction)
    {
        
        float delayBetweenShots = 1.0f / m_rateOfFire;
        m_nextShotDelay += Time.deltaTime;
        if (m_nextShotDelay > delayBetweenShots)
        {
            Vector3 enemyPos = GetNearestEnemyPosition(origin);
            if (enemyPos == Vector3.zero)
            {
                Shoot(origin, direction);
            }
            else
            {
                Shoot(origin, enemyPos);
            }
         
            m_nextShotDelay -= delayBetweenShots;
        }
    }

    private Vector3 GetNearestEnemyPosition(Vector3 origin)
    {
      var enemies = Object.FindObjectsOfType<EnemyComponent>();
      
      Vector3 bestTarget = Vector3.zero;
      float closestDistanceSqr = Mathf.Infinity;
      foreach(var potentialTarget in enemies)
      {
          Vector3 directionToTarget = potentialTarget.transform.position - origin;
          float dSqrToTarget = directionToTarget.sqrMagnitude;
          if(dSqrToTarget < closestDistanceSqr)
          {
              closestDistanceSqr = dSqrToTarget;
              bestTarget = potentialTarget.transform.position;
          }
      }

      bestTarget = new Vector3(bestTarget.x, origin.y, bestTarget.z);
      bestTarget -= origin;
      return bestTarget.normalized;
    }

}