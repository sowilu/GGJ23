using System;
using UnityEngine;

public class BounceBullet : MonoBehaviour
{
    public float ttl = 5f;
    public GameObject explosionPrefab;

    private void Start()
    {
        ttl +=  UnityEngine.Random.Range(-0.2f, 0.2f);
    }

    private void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*if (collision.relativeVelocity.magnitude > 3)
        {
            Boom();
        }*/
        
        if(collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("Player"))
        {
            Die();
        }
    }

    void Boom()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }

    void Die()
    {
        Boom();
        Destroy(gameObject);
    }
}
