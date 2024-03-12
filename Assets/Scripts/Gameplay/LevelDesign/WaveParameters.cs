using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Waves/Parameters")]
public class WaveParameters : ScriptableObject
{
    [Serializable]
    public class WaveEnemy
    {
        public EnemyComponent Enemy;
        public int Count;
    }

    [Serializable]
    public class Wave
    {
        public List<WaveEnemy> Enemies = new List<WaveEnemy>();
    }
    
    public float WaveDuration;
    public List<Wave> Waves;
}