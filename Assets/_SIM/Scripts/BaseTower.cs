using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SimVik;
using UnityEngine;
using Random = UnityEngine.Random;

public class BaseTower : MonoBehaviour
{
    public AudioClip shootSound;
    
    [Header("Shooting")]
    public float range = 5f;
    public GameObject bulletPrefab;
    public float coolDown = 1f;

    [Header("Body")] 
    public Transform body;
    public Transform cannon;
    
    //public OptiRingMesh _mesh;
    protected SphereCollider _sphereCollider;
    protected List<Transform> _inRangeEnemies = new List<Transform>();
    
    AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        TowerManager.inst.AddTower(transform);
        transform.DOScale(Vector3.one, 0.3f).ChangeStartValue(new Vector3(1, 0, 1)).SetEase(Ease.OutExpo);
        
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.isTrigger = true;
        _sphereCollider.radius = range;
        //_mesh.radius = range;
        
        body.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        
        InvokeRepeating(nameof(Activate), coolDown, coolDown);
    }

    protected virtual void Activate()
    {
        if (_inRangeEnemies.Count > 0)
        {
            //target first enemy
            var target = _inRangeEnemies[0];
            //taRGET RANDOM ENEMY
            //var target = _inRangeEnemies[Random.Range(0, _inRangeEnemies.Count)];
            var bullet = Instantiate(bulletPrefab, cannon.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().target = target;
            _audioSource.PlayOneShot(shootSound, Random.Range(0.9f, 1.2f));
            
            transform.DOScaleY( 1.2f, 0.1f).SetLoops(2, LoopType.Yoyo);
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
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            _inRangeEnemies.Remove(other.transform);
        }
    }

    protected virtual void Update()
    {
        if(_inRangeEnemies.Count > 0)
        {
            //target first enemy
            var target = _inRangeEnemies[0];

            if (target == null)
            {
                _inRangeEnemies.RemoveAt(0);
                return;
            }
            
            //rotate body towards target but only on y axis
            var targetRotation = Quaternion.LookRotation(target.position - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            body.rotation = Quaternion.Slerp(body.rotation, targetRotation, Time.deltaTime * 15f);
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

