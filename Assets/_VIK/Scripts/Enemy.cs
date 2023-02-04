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
    

    private NavMeshAgent _agent;

    void Start()
    {
        if(target == null)target = GameObject.FindWithTag("Target").transform;
        _agent = GetComponent<NavMeshAgent>();
        
        health = GetComponent<Health>();
        health.onDeath.AddListener(() => { WaveManager.enemies.Remove(this);});
    }

    void Update()
    {
        _agent.SetDestination(target.position);
        _agent.speed = speed;

        if (Vector3.Distance(transform.position, target.position) < 1f)
        {
            Attack();
        }
    }


    public void Attack()
    {
        print( "Attacking");
    }
}