using UnityEngine;
using UnityEngine.AI;

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
    

    private NavMeshAgent _agent;

    void Start()
    {

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
            health.Die();
        }
    }
}