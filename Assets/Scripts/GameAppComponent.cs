using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameAppComponent : MonoBehaviour
{
    public int CurrentWaveIndex;
    public WaveParameters.Wave CurrentWave;

    public bool IsGameActive;
    public float WaveTimer;

    private Dictionary<WaveParameters.WavePart, EnemyComponent> m_selectedEnemies;
    private Dictionary<WaveParameters.WavePart, float> m_enemiesSpawnTimers;
    private WaveParameters m_waveParameters;
    private GameData m_gameData;

    private UIManager m_uiManager;
    private PlayerComponent m_player;

    private System.Random m_random;

    public void Start()
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        
        DontDestroyOnLoad(gameObject);
        
        m_random = new System.Random();
        m_gameData = Addressables.LoadAssetAsync<GameData>("Assets/Data/GameData.asset").WaitForCompletion();
        m_waveParameters = m_gameData.WaveParameters;

        var playerStart = GameObject.FindObjectOfType<PlayerStartLocation>();
        Vector3 startLocation = Vector3.zero;
        if (playerStart != null)
            startLocation = playerStart.transform.position;
        m_player = GameObject.Instantiate(m_gameData.PlayerPrefab);
        m_player.transform.position = startLocation;
         
        m_uiManager = FindObjectOfType<UIManager>();
        
        StartWave(0);
        IsGameActive = true;
    }

    public void Update()
    {
        if (IsGameActive == false)
            return;

        WaveTimer += Time.deltaTime;
        foreach (var wavePart in m_enemiesSpawnTimers.Keys.ToList())
        {
            m_enemiesSpawnTimers[wavePart] += Time.deltaTime;

            var spawnDelay = m_waveParameters.WaveDuration / wavePart.Count;
            if (m_enemiesSpawnTimers[wavePart] > spawnDelay)
            {
                SpawnEnemy(m_selectedEnemies[wavePart]);
                m_enemiesSpawnTimers[wavePart] -= spawnDelay;
            }
        }

        if (WaveTimer > m_waveParameters.WaveDuration)
        {
            StartCoroutine("MoveToNextWaveCoroutine");
            IsGameActive = false;
        }
    }

    void StartWave(int index)
    {
        CurrentWaveIndex = index;
        if (CurrentWaveIndex >= m_waveParameters.Waves.Count)
            CurrentWaveIndex = m_waveParameters.Waves.Count-1;
        CurrentWave = m_waveParameters.Waves[CurrentWaveIndex];

        m_selectedEnemies = new Dictionary<WaveParameters.WavePart, EnemyComponent>();
        m_enemiesSpawnTimers = new Dictionary<WaveParameters.WavePart, float>();
        foreach (var wavePart in CurrentWave.Parts)
        {
            var enemiesAtThreat = m_gameData.Enemies.Where(e => e.Threat == wavePart.Threat).ToList();
            if (enemiesAtThreat.Count > 0)
            {
                var enemy = enemiesAtThreat[m_random.Next(0, enemiesAtThreat.Count)];
                m_selectedEnemies[wavePart] = enemy.Prefab;
                m_enemiesSpawnTimers[wavePart] = 0;
            }
        }

        WaveTimer = 0;
    }

    void SpawnEnemy(EnemyComponent prefab)
    {
        var spawnLocations = GameObject.FindObjectsOfType<SpawnLocationComponent>();
        var spawner = spawnLocations[m_random.Next(0, spawnLocations.Length)];

        EnemyComponent enemy = GameObject.Instantiate<EnemyComponent>(prefab);
        enemy.transform.position = spawner.transform.position;
    }

    IEnumerator MoveToNextWaveCoroutine()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.5f);
        
        m_uiManager.ShowTitle();
        yield return new WaitForSecondsRealtime(0.5f);

        var weaponToUpgrade = PickWeaponToUpgrade();

        if (weaponToUpgrade != null)
        {
            m_uiManager.ShowSelectText(weaponToUpgrade.Name);
            yield return new WaitForSecondsRealtime(0.5f);
            
            m_uiManager.ShowButtons(weaponToUpgrade.UpgradeA.Name, weaponToUpgrade.UpgradeB.Name);

            while (m_uiManager.Selection == UIManager.SelectionResult.None)
                yield return null;

            if (m_uiManager.Selection == UIManager.SelectionResult.UpgradeA)
                m_player.ReplaceWeapon(weaponToUpgrade, weaponToUpgrade.UpgradeA);
            if (m_uiManager.Selection == UIManager.SelectionResult.UpgradeB)
                m_player.ReplaceWeapon(weaponToUpgrade, weaponToUpgrade.UpgradeB);
        }
        else
        {
            m_uiManager.ShowNoUpgradeText();
            yield return new WaitForSecondsRealtime(2.0f);
            }
        
        m_uiManager.HideAll();

        Time.timeScale = 1;
        IsGameActive = true;
        StartWave(CurrentWaveIndex + 1);
    }

    private BaseWeapon PickWeaponToUpgrade()
    {
        var playerWeapons = m_player.ActiveWeapons;
        var upgradableWeapon = new List<BaseWeapon>();
        foreach (var weapon in playerWeapons)
        {
            if (weapon.UpgradeA != null && weapon.UpgradeB != null)
                upgradableWeapon.Add(weapon);
        }

        if (upgradableWeapon.Count > 0)
        {
            var weaponToUpgrade = upgradableWeapon[m_random.Next(0, upgradableWeapon.Count)];
            return weaponToUpgrade;
        }

        return null;
    }
}