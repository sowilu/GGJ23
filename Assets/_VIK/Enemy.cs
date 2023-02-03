using UnityEngine;
using UnityEngine.AI;

// navmesh agent follows target 
public class Enemy : MonoBehaviour
{
    public Transform target;
    public float speed = 3;


    private NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
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