using UnityEngine;

public class Flower : MonoBehaviour
{
    private Health _health;
    public float scaleAmount = 0.1f;
    public float scaleSpeed = 3;

    private void Start()
    {
        _health = GetComponent<Health>();
    }


    private void Update()
    {
        // scale sin wave y axis
        var scale = Mathf.Sin(Time.time * scaleSpeed * 6.28f) * scaleAmount + 1;
        transform.localScale = new Vector3(1, scale, 1);
    }
}
