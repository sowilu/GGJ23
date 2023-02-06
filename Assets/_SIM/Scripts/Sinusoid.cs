using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sinusoid : MonoBehaviour
{
    public float speed = 10;
    public float height = 2;
    private Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position = startPos + new Vector3(0, Mathf.Sin(Time.deltaTime * speed) * height, 0) * Time.deltaTime;
    }
}
