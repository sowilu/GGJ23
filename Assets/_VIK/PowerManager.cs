using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    public List<PowerData> powers;
    public int selectCount = 3;
    
    public UpgradeScreen upgradeScreen;
    
    public static PowerManager instance;
    
    private void Awake()
    {
        instance = this;
    }
    
    public void Start()
    {
        OfferPowers();
    }

    public void OfferPowers()
    {
        // give three random non repeating powers from the list
        var pow = new List<PowerData>();
        for (int i = 0; i < selectCount; i++)
        {
            var index = Random.Range(0, powers.Count);
            pow.Add(powers[index]);
        }
        upgradeScreen.ShowPowers(pow);
    }
    
    public void ApplyPower( PowerData power)
    {
        // apply power
    }
}
