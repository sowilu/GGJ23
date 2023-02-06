using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Flowey : MonoBehaviour
{
    public static Flowey inst;

    
    
    public Transform player;
    public float range = 4;
    public PlayerController playerHands;

    public AudioClip cashier;
    
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

    private AudioSource _audioSource;
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

    private void Start()
    {
        if (price1 == null || price2 == null || price3 == null) return;
           
        price1.text = slot1Price.ToString();
        price2.text = slot2Price.ToString();
        price3.text = slot3Price.ToString();
        
        _audioSource = GetComponent<AudioSource>();
    }

    public void AddResources(int amount)
    {
        resourceCount += amount;
        resources.text = resourceCount.ToString();
    }
    
    public void BuySlot1()
    {
        if (playerHands.towerInHand == null && resourceCount >= slot1Price)
        {
            resourceCount -= slot1Price;
            resources.text = resourceCount.ToString();

            playerHands.towerInHand = tower1;
            //TODO: turn on seed model on back
            playerHands.seed.SetActive(true);
            
            _audioSource.PlayOneShot(cashier);
        }
    }
    
    public void BuySlot2()
    {
        if (playerHands.towerInHand == null && resourceCount >= slot2Price)
        {
            resourceCount -= slot2Price;
            resources.text = resourceCount.ToString();

            playerHands.towerInHand = tower2;
            //TODO: turn on seed model on back
            playerHands.seed.SetActive(true);
            _audioSource.PlayOneShot(cashier);
        }
    }
    
    public void BuySlot3()
    {
        if (playerHands.towerInHand == null && resourceCount >= slot3Price)
        {
            resourceCount -= slot3Price;
            resources.text = resourceCount.ToString();

            playerHands.towerInHand = tower3;
            //TODO: turn on seed model on back
            playerHands.seed.SetActive(true);
            _audioSource.PlayOneShot(cashier);
        }
    }

    public bool InRange(Vector3 position)
    {
        return Vector3.Distance(transform.position, position) < range;
    }

    void Update()
    {
        if(player == null) return;
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
