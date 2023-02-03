using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OneLine;
using UnityEngine;


// have a list of spawner objects
// each n wave enable new spawner
// new wave begins when all enemies are dead

[System.Serializable]
public class Wave
{
    public int enemies = 1;
    public float spawnRate = 1;
    public bool addHole = false;
    [OneLine] public List<EnemyRate> enemyTypes;
}

[System.Serializable]
public class EnemyRate
{
    public GameObject enemy;
    [Min(1)] public float chance = 1;
}

public class WaveManager : MonoBehaviour
{
    public List<Wave> waves;
    public List<Spawner> spawners;
    public List<Spawner> enabledSpawners;
    public static List<Enemy> enemies;
    
    private int currentWave = 0;
    private int currentSpawners;

    private void Awake()
    {
        // get all spawners in children
        spawners = GetComponentsInChildren<Spawner>().ToList();
        enabledSpawners = new List<Spawner>();
        enemies = new List<Enemy>();
    }

    IEnumerator WaveRoutine()
    {
        // get current wave deep copy
        var wave = waves[currentWave];

        if (wave.addHole)
        {
            AddHole();
        }
        
        // spawn enemies
        for (int i = 0; i < wave.enemies; i++)
        {
            // get random spawner
            var spawner = enabledSpawners[Random.Range(0, enabledSpawners.Count)];
            // get random enemy
            var enemy = wave.enemyTypes[Random.Range(0, wave.enemyTypes.Count)];
            // spawn enemy
            //spawner.Spawn(enemy.enemy);
            // wait for spawn rate
            yield return new WaitForSeconds(wave.spawnRate);
        }
    }

    void AddHole()
    {
        // to enabled spawners add one mroe that is not in the list
        var spawner = spawners.Except(enabledSpawners).ToList()[Random.Range(0, spawners.Count - enabledSpawners.Count)];
        enabledSpawners.Add(spawner);
    }
}