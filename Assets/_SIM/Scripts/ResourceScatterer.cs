using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourceScatterer : MonoBehaviour
{
    public static ResourceScatterer inst;

    public int height = 10;
    public GameObject resource;
    public int maxCount = 10;
    public float radius = 10f;

    public float spawnRate = 3;

    [HideInInspector]
    public int count = 0;

    private void Awake()
    {
        if(inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0, spawnRate);
    }

    void Spawn()
    {
        if (count < maxCount)
        {
            //spawn resource at random position
            Vector3 pos = new Vector3(Random.Range(-radius, radius), height, Random.Range(-radius, radius));
            Instantiate(resource, pos, Quaternion.identity);
        }
    }
}
