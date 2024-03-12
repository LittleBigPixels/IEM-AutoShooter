using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public enum ThreatLevel
    {
        Simple, Medium, Advanced, Boss
    }
    
    public String Name;
    public BaseEnemyComponent Prefab;
    public ThreatLevel Threat;
}