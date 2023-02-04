using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildVisualizer : MonoBehaviour
{
    public GameObject root;
    public GameObject tower;
    public PlayerController player;
    
    void Update()
    {
        if (player.towerInHand == null)
        {
            root.SetActive(false);
            tower.SetActive(false);
            return;
        }
        
        root.SetActive(true);
        tower.SetActive(true);
        
        var nearest = TowerManager.inst.GetNearest(player.transform.position + player.transform.forward);
        
        //rotate root from player to nearest
        root.transform.rotation = Quaternion.LookRotation(nearest.position - player.transform.position);
        root.transform.localScale = new Vector3(0, 0, Vector3.Distance(nearest.position, player.transform.position)/6);
        
        
        //place tower in front of the player
        tower.transform.position = player.transform.position + player.transform.forward;

    }
}
