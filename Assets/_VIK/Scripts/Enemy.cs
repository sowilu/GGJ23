using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[SelectionBase]
[DisallowMultipleComponent]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    public Transform target;
    public float speed = 3;
    Health health;
    public GameObject deathEffect;
    public bool dieOnHit;
    public bool dieOnPlayerHit;
    public GameObject explosionEffect;

    private NavMeshAgent _agent;

    private void OnDestroy()
    {
        Instantiate(explosionEffect, transform.position +   Vector3.up * 0.01f, Quaternion.Euler(-90,Random.Range(0,360f),0));
    }

    void Start()
    {
        GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
        if(target == null)target = GameObject.Find("Flowey").transform;;
        _agent = GetComponent<NavMeshAgent>();
        
        health = GetComponent<Health>();
        health.onDeath.AddListener(() => { WaveManager.enemies.Remove(this);});
    }

    void Update()
    {
        _agent.SetDestination(target.position);
        _agent.speed = speed;
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        print( "Enemy collided with " + collision.gameObject.name );
        if (collision.gameObject.CompareTag("Player") && dieOnPlayerHit)
        {
            health.Die();
        }
        
        if (collision.gameObject.name == ("Flowey") && dieOnHit)
        {
            collision.gameObject.GetComponent<Health>().hp -= 1;
            health.Die();
        }
    }
}