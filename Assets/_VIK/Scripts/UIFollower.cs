using UnityEngine;

[ExecuteAlways]
public class UIFollower : MonoBehaviour
{
    public Transform target;
    private RectTransform rt;
    public Vector3 offset;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }


    private void LateUpdate()
    {
        if (target == null) return;
       
        
        rt.position = Camera.main.WorldToScreenPoint(target.position) + offset;
        

    }
}
