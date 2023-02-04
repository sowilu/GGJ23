using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Flowey : MonoBehaviour
{
    public static Flowey inst;

    
    
    public Transform player;
    public float range = 4;
    public PlayerController playerHands;
    
    
    [Header("UI")]
    public GameObject choicesMenu;
    public TextMeshProUGUI resources;
    
    [Header("Slot 1")]
    public Image slot1Image;
    public TextMeshProUGUI price1;
    public BaseTower tower1;
    public int slot1Price;
    
    [Header("Slot 2")]
    public Image slot2Image;
    public TextMeshProUGUI price2;
    public BaseTower tower2;
    public int slot2Price;
    
    [Header("Slot 3")]
    public Image slot3Image;
    public TextMeshProUGUI price3;
    public BaseTower tower3;
    public int slot3Price;

    
    int resourceCount = 0;
    
    private void Awake()
    {
        //instantiate the singleton
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void AddResources(int amount)
    {
        resourceCount += amount;
        resources.text = resourceCount.ToString();
    }
    
    public void BuySlot1()
    {
        if (playerHands.towerInHand == null)
        {
            playerHands.towerInHand = tower1;
        }
    }
    
    public void BuySlot2()
    {
        
    }
    
    public void BuySlot3()
    {
        
    }

    public bool InRange(Vector3 position)
    {
        return Vector3.Distance(transform.position, position) < range;
    }

    void Update()
    {
        if (InRange(player.position))
        {
            choicesMenu.SetActive(true);
            playerHands.choiceMode = true;
        }
        else
        {
            choicesMenu.SetActive(false);
            playerHands.choiceMode = false;
        }
    }
}
