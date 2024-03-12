using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Waves/Parameters")]
public class WaveParameters : ScriptableObject
{
    [Serializable]
    public class WavePart
    {
        public EnemyData.ThreatLevel Threat;
        public int Count;
    }

    [Serializable]
    public class Wave
    {
        [FormerlySerializedAs("Enemies")] public List<WavePart> Parts = new List<WavePart>();
    }
    
    public float WaveDuration;
    public List<Wave> Waves;
}