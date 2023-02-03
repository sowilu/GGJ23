using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SimVik;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    public float range = 5f;
    public GameObject bulletPrefab;
    
    public float coolDown = 1f;

    protected OptiRingMesh _mesh;
    protected SphereCollider _sphereCollider;
    protected List<Transform> _inRangeEnemies = new List<Transform>();
    
    void Start()
    {
        transform.DOScale(Vector3.one, 0.3f).ChangeStartValue(new Vector3(1, 0, 1)).SetEase(Ease.OutExpo);
        
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.isTrigger = true;
        _sphereCollider.radius = range;
        _mesh = GetComponent<OptiRingMesh>();

        _mesh.radius = range;
        
        InvokeRepeating(nameof(Activate), coolDown, coolDown);
    }

    protected virtual void Activate()
    {
        if (_inRangeEnemies.Count > 0)
        {
            //target first enemy
            var target = _inRangeEnemies[0];
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().target = target;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //check if enemy
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            _inRangeEnemies.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //check if enemy
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player") )
        {
            _inRangeEnemies.Remove(other.transform);
        }
    }
    
    
    //if in unity editor mode
    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    #endif
}

