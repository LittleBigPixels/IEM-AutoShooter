using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;

    private int m_value;

    public static GameManager Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = Find();
            return m_instance;
        }
    }

    public PlayerComponent Player;
    public List<EnemyComponent> Enemies;
    public List<SpawnLocationComponent> SpawnLocations;

    private static GameManager Create()
    {
        var go = new GameObject("GameManager");
        return go.AddComponent<GameManager>();
    }

    private static GameManager Find()
    {
        return GameObject.FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        SpawnLocations = GameObject.FindObjectsOfType<SpawnLocationComponent>().ToList();
        //Value = 5;
    }

    private void Update()
    {
        //Game happen here
    }
}