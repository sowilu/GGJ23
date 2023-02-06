using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Deflower : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip scream;
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

        if (petals.Count == 0)
        {
            audio.PlayOneShot(scream);
            Invoke(nameof(Die), scream.length);
        }
    }

    void Die()
    {
        SceneManager.LoadScene(2);
    }

    void Reset()
    {
        canBeHarmed = true;
    }
}
