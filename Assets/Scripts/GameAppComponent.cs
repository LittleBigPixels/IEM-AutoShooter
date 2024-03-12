using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

public class GameAppComponent : MonoBehaviour
{
    public int CurrentWaveIndex;
    public WaveParameters.Wave CurrentWave;

    public bool IsGameActive;
    public float WaveTimer;

    private Dictionary<WaveParameters.WaveEnemy, float> m_enemiesSpawnTimers;
    private WaveParameters m_waveParameters;

    private UIManager m_uiManager;
    private PlayerComponent m_player;

    private System.Random m_random;

    public void Start()
    {
        m_random = new System.Random();
        m_waveParameters = Addressables.LoadAssetAsync<WaveParameters>("Assets/Data/Waves.asset").WaitForCompletion();

        m_uiManager = FindObjectOfType<UIManager>();
        m_player = FindObjectOfType<PlayerComponent>();
        
        StartWave(0);
        IsGameActive = true;
    }

    public void Update()
    {
        if (IsGameActive == false)
            return;

        WaveTimer += Time.deltaTime;
        foreach (var waveEnemy in m_enemiesSpawnTimers.Keys.ToList())
        {
            m_enemiesSpawnTimers[waveEnemy] += Time.deltaTime;

            var spawnDelay = m_waveParameters.WaveDuration / waveEnemy.Count;
            if (m_enemiesSpawnTimers[waveEnemy] > spawnDelay)
            {
                SpawnEnemy(waveEnemy.Enemy);
                m_enemiesSpawnTimers[waveEnemy] -= spawnDelay;
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

        m_enemiesSpawnTimers = new Dictionary<WaveParameters.WaveEnemy, float>();
        foreach (var waveEnemy in CurrentWave.Enemies)
            m_enemiesSpawnTimers[waveEnemy] = 0;

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