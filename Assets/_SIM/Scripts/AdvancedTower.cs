using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedTower : BaseTower
{
    public Transform cannon2;
    public Transform cannon3;
    public Transform cannon4;
    
    protected override void Activate()
    {
        Instantiate(bulletPrefab, cannon.position, cannon.rotation);
        Instantiate(bulletPrefab, cannon2.position, cannon2.rotation);
        Instantiate(bulletPrefab, cannon3.position, cannon3.rotation);
        Instantiate(bulletPrefab, cannon4.position, cannon4.rotation);
    }
}
