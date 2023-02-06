using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public Transform target;
    
    public float speed = 10f;
    public int damage = 20;

    public float ttl = 2f;

    private void Start()
    {
        if(target != null)
            transform.LookAt(target.position);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        if (ttl <= 0)
        {
            Die();
        }
        ttl -= Time.deltaTime;
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
               // Destroy(other.gameObject);
            }

            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
