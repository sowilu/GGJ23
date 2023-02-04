using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflower : MonoBehaviour
{
    public List<GameObject> petals;

    public float invinsibilityTime = 1;

    private bool canBeHarmed = true;
    
    public void TakeDamage(int damage)
    {
        if (canBeHarmed)
        {
            petals[0].SetActive(false);
            petals.RemoveAt(0);
            canBeHarmed = false;
            Invoke(nameof(Reset), invinsibilityTime);
        }
    }

    void Reset()
    {
        canBeHarmed = true;
    }
}
