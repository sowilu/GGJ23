using System.Collections.Generic;
using UnityEngine;

public class UpgradeScreen : UIScreen
{
    public PowerCard cardPrefab;
    public List<PowerCard> cards;
    public Transform cardParent;

    [SerializeField] private AudioClip cardPopSound;
    
    
    public static UpgradeScreen instance;
    
    private void Awake()
    {
        instance = this;
    }

    public async void ShowPowers(List<PowerData> powers)
    {
        foreach (var power in powers)
        {
            SpawnCard(power);
            await new WaitForSeconds(0.5f);
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