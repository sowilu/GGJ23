using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;




public class WaveManager : MonoBehaviour
{
    [Header("Spawners")]
    public EnemyWaves enemyWaves;
    public List<Spawner> spawners;
    public List<Spawner> enabledSpawners;
    public static List<Enemy> enemies;
    
    [Header("Wave UI")]
    [SerializeField]AudioClip waveStartSound;
    [SerializeField]AnnounceText announceText;

    public int currentWave = 0;
    private int currentSpawners;
    public bool waveInProgress = false;
    public static UnityEvent<int> OnWaveFinished = new UnityEvent<int>();

    private void Awake()
    {
        // get all spawners in children
        spawners = GetComponentsInChildren<Spawner>().ToList();
        enabledSpawners = new List<Spawner>();
        enemies = new List<Enemy>();
    }

    private void Update()
    {
        // if wave is not in progress and there are no enemies
        if (!waveInProgress && enemies.Count == 0)
        {
            OnWaveFinished.Invoke(currentWave);
            // start new wave
            StartCoroutine(WaveRoutine());
        }
    }

    IEnumerator WaveRoutine()
    {
        Audio.Play( waveStartSound );
        waveInProgress = true;
        // get current wave deep copy
        var wave = enemyWaves.waves[currentWave];

        if (wave.addHole)
        {
            AddHole();
        }
        
        // get all enemies in wave
        var enemies = wave.enemyTypes.SelectMany(enemy => Enumerable.Repeat(enemy.type, (int)enemy.number)).ToList();
        
        // spawn enemies
        while (enemies.Count > 0)
        {
            // get random spawner
            var spawner = enabledSpawners[Random.Range(0, enabledSpawners.Count)];
            // get random enemy
            var enemy = enemies[Random.Range(0, enemies.Count)];
            // spawn enemy
            spawner.Spawn(enemy);
            // remove enemy from list
            enemies.Remove(enemy);
            // wait for next spawn
            yield return new WaitForSeconds(1f/wave.spawnRate);
        }
        print("Wave finished spawning");
        waveInProgress = false;
        currentWave++;
        announceText.ShowMessage($"Wave {currentWave + 1}");
    }



    void AddHole()
    {
        var newHole =
            spawners.Except(enabledSpawners).ToList()[Random.Range(0, spawners.Count - enabledSpawners.Count)];
        newHole.isActive = true;
        enabledSpawners.Add(newHole);
    }
}