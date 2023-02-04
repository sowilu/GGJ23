using UnityEngine;

public class SlowMotion : MonoBehaviour
{

    public float slowDownFactor = 0.05f;
    
    
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Time.timeScale = slowDownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
        else
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }
}
