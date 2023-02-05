using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedTower : BaseTower
{
    public Transform cannon2;
    public Transform cannon3;
    public Transform cannon4;
    
    public float forwardForce = 10;
    public float upForce = 10;
    
    protected override void Activate()
    {
        var obj = Instantiate(bulletPrefab, cannon.position, cannon.rotation);
        // add force forward
        obj.GetComponent<Rigidbody>().AddForce(cannon.forward * forwardForce + Vector3.up  *upForce, ForceMode.Impulse);
        
        obj = Instantiate(bulletPrefab, cannon2.position, cannon2.rotation);
        obj.GetComponent<Rigidbody>().AddForce(cannon2.forward * forwardForce + Vector3.up  *upForce, ForceMode.Impulse);
        
        obj = Instantiate(bulletPrefab, cannon3.position, cannon3.rotation);
        obj.GetComponent<Rigidbody>().AddForce(cannon3.forward * forwardForce + Vector3.up  *upForce, ForceMode.Impulse);
        
        obj = Instantiate(bulletPrefab, cannon4.position, cannon4.rotation);
        obj.GetComponent<Rigidbody>().AddForce(cannon4.forward * forwardForce + Vector3.up  *upForce, ForceMode.Impulse);
    }
}
