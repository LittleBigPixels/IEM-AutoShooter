using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public class RatEnemyComponent : BaseEnemyComponent
    {
        [SerializeField] private float Speed;
        [SerializeField] private float SpeedRandomness;
        [SerializeField] private float WiggleSpeed;
        [SerializeField] private float WiggleSpeedRandomness;
        [SerializeField] private float WiggleAmount;
        
        private PlayerComponent _mPlayer;
        private float _spawnedTime = 0f;
        
        public void Start()
        {
            Speed += Random.Range(-1, 1) * SpeedRandomness;
            WiggleSpeed += Random.Range(-1, 1) * WiggleSpeedRandomness;
            _spawnedTime = Time.time;

            _mPlayer = FindObjectOfType<PlayerComponent>();
        }

        public void Update()
        {
            Vector3 directionToPlayer = _mPlayer.transform.position - transform.position;

            float angle = WiggleAmount * Mathf.Cos(WiggleSpeed * 2f * Mathf.PI * Time.time + _spawnedTime);
            
            var eulerAngle = new Vector3(0, angle, 0);
            
            Quaternion rotationOffset = Quaternion.Euler(eulerAngle);
            
            directionToPlayer = rotationOffset * directionToPlayer.normalized;
            
            transform.localRotation = Quaternion.LookRotation(directionToPlayer);
            
            transform.position += Time.deltaTime * Speed * directionToPlayer;
        }
    }
}
