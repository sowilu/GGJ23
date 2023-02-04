using System.Collections.Generic;
using OneLine;
using UnityEngine;

[CreateAssetMenu(fileName = "Waves", menuName = "Data/Waves")]
public class EnemyWaves : ScriptableObject
{
    public List<Wave> waves;
}



[System.Serializable]
public class Wave
{
    public float spawnRate = 1;
    public bool addHole = false;
    [OneLine] public List<EnemyRate> enemyTypes;
}


[System.Serializable]
public class EnemyRate
{
    public Enemy type;
    [Min(1)] public float number = 1;
}