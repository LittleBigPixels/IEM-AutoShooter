using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data")]
public class GameData : ScriptableObject
{
    public PlayerComponent PlayerPrefab;
    public WaveParameters WaveParameters;
    public List<EnemyData> Enemies;
}