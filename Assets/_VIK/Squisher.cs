using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squisher : MonoBehaviour
{
    public float scaleAmount = 0.1f;
    public float scaleSpeed = 3;
    public Vector3 startScale;

    private void Start()
    {
        startScale = transform.localScale;
    }


    private void Update()
    {
        // scale sin wave y axis
        var scale = Mathf.Sin(Time.time * scaleSpeed * 6.28f) * scaleAmount + 1;
        transform.localScale = new Vector3( startScale.x, startScale.y * scale, startScale.z);
    }
}
