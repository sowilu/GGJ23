
using System;
using UnityEngine;

public class PlaneShadow : MonoBehaviour
{
    public Transform target;
    public float height = 0.1f;


    private void Awake()
    {
        if(target == null)target = transform.parent;
    }


    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, height, target.position.z);
            transform.rotation = Quaternion.Euler(90,0, 0);
        }
            
    }
}
