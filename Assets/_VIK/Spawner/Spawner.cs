using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float range = 3;
    
    bool _isActive;
    public bool isActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            if (value)
            {
                OnActivate();
            }
        }
    }

    public Transform hole;

    private void Start()
    {
        hole.localScale = Vector3.zero;
    }

    public void OnActivate()
    {
        hole.DOScale(Vector3.one, 2f);
    }


    public void Spawn(Enemy prefab)
    {
        var pos = transform.position + UnityEngine.Random.insideUnitSphere * range;
        var enemy = Instantiate(prefab, pos, Quaternion.identity);
        WaveManager.enemies.Add(enemy);
    }
    
    
    


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (isActive) Handles.color = Color.red; 
        else Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.up, range);
    }
#endif
}
