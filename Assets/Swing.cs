using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Swing : MonoBehaviour
{
    public Vector3 power;
    public Vector3 startPos;

    public float speed;
    private float timeOffset;

    private void Start()
    {
        timeOffset = Random.Range(0, 100);
    }


    private void Update()
    {
        transform.localPosition = power * Mathf.Sin(Time.time * speed + timeOffset) + startPos;
    }
}
