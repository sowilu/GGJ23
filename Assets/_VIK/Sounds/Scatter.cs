using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Scatter : MonoBehaviour
{
    public List<GameObject> prefabs;

    [FormerlySerializedAs("density")] public float stepSize = 1;
    public float offset;
    public TerrainCollider terrain;

    public Bounds bounds;

    public Vector3 rotatationOffset;

    public Vector2 randomScale = Vector2.one;
    // get bounds of target and in bounds xz plane scatter objects
    
    public Vector2 angleRange;
    public float planeProbability = 0.5f;
    public float normalOffset = -0.1f;
    
    public LayerMask mask;


    private void OnValidate()
    {
        if (terrain != null)
        {
            bounds = terrain.bounds;
        }
    }

    void Start()
    {
        Populate();

    }

    public void Populate()
    {
        
        for (float x = bounds.min.x; x < bounds.max.x; x += stepSize)
        {
            for (float z = bounds.min.z; z < bounds.max.z; z += stepSize)
            {
                Vector3 pos = new Vector3(x, 10, z);
                pos += (Random.insideUnitCircle * offset).X0Z();
                if (Physics.Raycast(pos, Vector3.down, out RaycastHit hit, 50,mask))
                {
                    
                    // check if hit normal is between angle range
                    var angle = Vector3.Angle(hit.normal, Vector3.up);
                    if ( angle < angleRange.x || angle > angleRange.y )
                    {
                        var chance = Random.value;
                        if(chance > planeProbability) continue;
                    }
                        
                    
                    
                    pos = hit.point + hit.normal * normalOffset;
                    var rot = Quaternion.Euler(new Vector3(Random.Range( -rotatationOffset.x, rotatationOffset.x), Random.Range( -rotatationOffset.y, rotatationOffset.y), Random.Range( -rotatationOffset.z, rotatationOffset.z)));
                    var obj = Instantiate(prefabs[Random.Range(0, prefabs.Count)], pos, rot);
                    obj.transform.localScale = Vector3.one * Random.Range(randomScale.x, randomScale.y);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
}

// vector 3 extensions to convert xyz to xz

public static class Vector3Extensions
{
    public static Vector2 XZ(this Vector3 v)
    {
        return new Vector2(v.x, v.z);
    }
    
    public static Vector3 X0Z(this Vector2 v)
    {
        return new Vector3(v.x, 0, v.y);
    }
}
