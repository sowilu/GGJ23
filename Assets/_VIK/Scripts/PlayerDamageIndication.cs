using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageIndication : MonoBehaviour
{
    public bool takeRealDamage = false;
    Health health;
    PlayerController playerController;

    private void Start()
    {
        health = GetComponent<Health>();
        playerController = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bullet") || collision.transform.CompareTag("Enemy") ||
            collision.transform.CompareTag("Root"))
        {
            playerController.TakeDamage(1);
            
            if(takeRealDamage)
                health.hp -= 10;
        }
    }
}
