
using UnityEngine;
using Random = UnityEngine.Random;

public class SawTower : BaseTower
{
    [Header("Saw settings")]
    public AudioClip sawClip;
    public float rotationSpeed;
    public int damage = 30;
    public GameObject sawParticles;
    public AudioSource audio;
    
    protected override void Activate()
    {
        foreach (var enemy in _inRangeEnemies)
        {
            var health = enemy.GetComponent<Health>();
            if (health != null)
            {
                health.hp -= damage;
            }
            
        }

        audio.pitch = Random.Range(0.8f, 1.2f);
    }

    protected override void Update()
    {
        if (_inRangeEnemies.Count > 0)
        {
            body.Rotate(0, rotationSpeed * Time.deltaTime, 0);

            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }
        else
        {
            audio.Stop();
        }
    }
}
