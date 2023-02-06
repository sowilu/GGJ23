using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScaler : MonoBehaviour
{
    public GameObject healthBar;
    public Health health;
    
    void Update()
    {
        healthBar.transform.localScale = new Vector3(health.hp / health.maxHP, 1, 1);
    }
}
