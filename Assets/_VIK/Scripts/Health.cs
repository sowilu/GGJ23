using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHP = 100;
    private int _hp;
    
    public UnityEvent<int> onDamage = new UnityEvent<int>();
    public UnityEvent onDie = new UnityEvent();
    public bool autoDestroy = true;
    public bool tweenDamage = true;
    
    public int hp
    {
        get => _hp;
        set
        {
            _hp = value;
            var damage = maxHP - _hp;
            onDamage.Invoke(damage);
            
            if (_hp <= 0)
            {
                Die();
            }
        }
    }

    private void Start()
    {
        if (tweenDamage)
        {
            onDamage.AddListener(damage =>
            {
                // short white flash material tween
                var mat = GetComponent<Renderer>().material;
                mat.DOColor(Color.white, 0.1f).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
                {
                    mat.color = Color.white;
                });
            });
        }
    }

    public void Die()
    {
        onDie.Invoke();
        if( autoDestroy )
            Destroy(gameObject);
    }
}
