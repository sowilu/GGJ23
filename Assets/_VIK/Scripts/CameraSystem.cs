
using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[ExecuteAlways]
public class CameraSystem : MonoBehaviour
{
    public List<Transform> targets;
    public float distance = 15;
    static  Transform camTransform;
    public Vector3 offset;
    public bool ignoreYAxis;

    private void Awake()
    {
        camTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (targets.Count == 0)
            return;
        
        Vector3 centerPoint = GetCenterPoint();
        
        if( ignoreYAxis )
            centerPoint.y = 0;

        transform.position = centerPoint + offset;
    }
    
    private Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }

    public static void Shake()
    {
        camTransform.DOShakePosition(0.5f, 0.5f, 10, 90, false, true);
    }
}
