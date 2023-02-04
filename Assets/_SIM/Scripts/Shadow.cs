using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public GameObject player;
    void Update()
    {
        //follow player but stay on the ground
        transform.position = new Vector3(player.transform.position.x, 0.01f, player.transform.position.z);
    }
}
