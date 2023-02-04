using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollen : MonoBehaviour
{
    public float fallSpeed = 1;
    public float collectSpeed = 3;
        
    bool inAir = true;
    private Transform target;
    
    void Update()
    {
        //fall until ground is reached
        if (inAir)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }
        //if there is target, move towards it
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, collectSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            inAir = false;
        }
        else if (other.transform.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            
            if (player.TakeResource())
            {
                ResourceScatterer.inst.count--;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player.resourcesInHand < player.maxResourcesInHand)
            {
                target = other.transform;
            }
        }
    }
}
