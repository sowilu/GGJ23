using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHP = 100;
    private int _hp;
    
    public UnityEvent<int> onDamage = new UnityEvent<int>();
    public UnityEvent onDie = new UnityEvent();
    public bool autoDestroy = true;
    
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

    public void Die()
    {
        onDie.Invoke();
        if( autoDestroy )
            Destroy(gameObject);
    }
}
