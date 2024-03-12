using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public enum ThreatLevel
    {
        Simple, Medium, Advanced, Boss
    }
    
    public String Name;
    public EnemyComponent Prefab;
    public ThreatLevel Threat;
}