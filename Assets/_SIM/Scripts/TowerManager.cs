using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager inst;

    public GameObject root;
    public float rootLength;
    
    [Header("Add flowey as first tower")]
    public List<Transform> towers = new List<Transform>();

    private void Awake()
    {
        //instantiate singleton
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void AddTower(Transform tower)
    {
        //pass variable to coroutine
        StartCoroutine(GrowRoots(tower));

        towers.Add(tower);
    }

    public Transform GetNearest(Vector3 position)
    {
        Transform nearestTower = towers[0];
        float nearestDistance = Vector3.Distance(position, nearestTower.position);
        for (int i = 1; i < towers.Count; i++)
        {
            float distance = Vector3.Distance(position, towers[i].position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestTower = towers[i];
            }
        }

        return nearestTower;
    }
    
    
    //coroutine
    public IEnumerator GrowRoots(Transform tower)
    {
        //connect with nearest tower with root segments
        if (towers.Count > 0)
        {
            Transform nearestTower = towers[0];
            float nearestDistance = Vector3.Distance(tower.position, nearestTower.position);
            for (int i = 1; i < towers.Count; i++)
            {
                float distance = Vector3.Distance(tower.position, towers[i].position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestTower = towers[i];
                }
            }

            //create as many roots as will fit in given distance and spawn them in between
            int rootCount = Mathf.CeilToInt(nearestDistance / rootLength);
            Transform firstRoot = null;
            Transform lastRoot = null;

            if (rootCount > 1)
            {
                for (int i = 0; i < rootCount; i++)
                {
                    Vector3 spawnPos = Vector3.Lerp(nearestTower.position + Vector3.down, tower.position + Vector3.down, (float)i / rootCount);
                    //rotate root to face tower
                    Quaternion spawnRot = Quaternion.LookRotation(tower.position + Vector3.down - (nearestTower.position + Vector3.down));
    
                    if (i == 0)
                    {
                        firstRoot = Instantiate(root, spawnPos, spawnRot).transform;
                        lastRoot = firstRoot;
                    }
                    else
                    {
                        lastRoot = Instantiate(root, spawnPos, spawnRot, firstRoot).transform;
                    }
                    
                }
            }
            else
            {
                firstRoot = lastRoot = Instantiate(root, nearestTower.position + Vector3.down, Quaternion.LookRotation(tower.position + Vector3.down - (nearestTower.position + Vector3.down))).transform;
            }
            

            //scale last root to fit
            if (lastRoot != null)
            {
                var rootDistance = Vector3.Distance(lastRoot.position, tower.position);
                lastRoot.transform.localScale = new Vector3(1, 1, rootDistance/rootLength);
            }
            
            
            yield return new WaitForSeconds(0.3f);

            while (firstRoot.position.y < 0)
            {
                firstRoot.transform.position += Vector3.up * Time.deltaTime;
                yield return new WaitForSeconds(0.003f);
            }
        }
    }
}
