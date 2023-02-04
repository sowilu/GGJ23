using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHP = 100;
    private int _hp;
    
    public UnityEvent<int> onDamage = new UnityEvent<int>();
    public UnityEvent onDeath = new UnityEvent();
    public bool autoDestroy = true;
    public bool tweenDamage = true;
    public GameObject deathEffect;
    public GameObject damageEffect;

    public int hp
    {
        get => _hp;
        set
        {
            _hp = value;
            var damage = maxHP - _hp;
            Damage(damage);
            
            if (_hp <= 0)
            {
                Die();
            }
        }
    }

    private void Start()
    {

    }

    void Damage(int damage)
    {
        onDamage.Invoke(damage);
        if (tweenDamage)
        {
            transform.DOScale( 1.3f, 0.1f).SetLoops(2, LoopType.Yoyo);
        }
        if (damageEffect != null) Instantiate(damageEffect, transform.position, Quaternion.identity);
    }

    public void Die()
    {
        onDeath.Invoke();
        if(deathEffect != null) Instantiate(deathEffect, transform.position, Quaternion.identity);


        if (!autoDestroy) return;
        foreach (var ps in GetComponentsInChildren<ParticleSystem>())
        {
            ps.transform.parent = null;
        }
        Destroy(gameObject);
    }
}
