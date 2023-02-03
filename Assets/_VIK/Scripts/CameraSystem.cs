
using UnityEngine;

[ExecuteAlways]
public class CameraSystem : MonoBehaviour
{
    public Transform target;
    public float distance = 15;
    
    private void LateUpdate()
    {
        if( target != null)
            transform.position = target.position;
        
        // look at target
        transform.LookAt(target);
        

    }
}
