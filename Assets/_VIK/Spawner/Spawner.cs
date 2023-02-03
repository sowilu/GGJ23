using System;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float range = 3;
    public Enemy prefab;
    


    public void Spawn()
    {
        var pos = transform.position + UnityEngine.Random.insideUnitSphere * range;
        var enemy = Instantiate(prefab, pos, Quaternion.identity);
        WaveManager.enemies.Add(enemy);
    }
    
    
    


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, range);
    }
#endif
}
