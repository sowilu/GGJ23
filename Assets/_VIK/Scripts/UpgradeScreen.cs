using System.Collections.Generic;
using UnityEngine;

public class UpgradeScreen : UIScreen
{
    public PowerCard cardPrefab;
    public List<PowerCard> cards;
    public Transform cardParent;

    [SerializeField] private AudioClip cardPopSound;
    public float cardPopDelay = 0.5f;
    public float cardPopSpeed = 0.5f;
    
    
    public static UpgradeScreen instance;
    
    private void Awake()
    {
        instance = this;
    }

    public async void ShowPowers(List<PowerData> powers)
    {
        await new WaitForSeconds(cardPopDelay);
        foreach (var power in powers)
        {
            SpawnCard(power);
            await new WaitForSeconds(cardPopSpeed);
        }
    }



    public void SpawnCard(PowerData power)
    {
        var card = Instantiate(cardPrefab, cardParent);
        cards.Add(card);
        card.powerData = power;
        Audio.Play(cardPopSound);
    }
    
    public override void Open()
    {
        base.Open();
        gameObject.SetActive(true);
    }
    
    
    public override void Close()
    {
        // destroy all cards
        foreach (var card in cards)
        {
            Destroy(card.gameObject);
        }
        cards.Clear();
        
        base.Close();
        gameObject.SetActive(false);
    }
}


public class UIScreen : MonoBehaviour
{
    public virtual void Open()
    {
        
    }


    public virtual void Close()
    {
        
    }
}