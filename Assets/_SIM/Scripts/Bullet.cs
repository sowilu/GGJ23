using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public Transform target;
    
    public float speed = 10f;
    public int damage = 5;
    
    void Update()
    {
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position + Vector3.up * 0.5f, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")
        {
            var health = other.gameObject.GetComponent<Health>();
            if(health != null)
            {
                health.hp -= damage;
            }
            else
            {
                Destroy(other.gameObject);
            }
            Destroy(gameObject);
        }

    }
}
