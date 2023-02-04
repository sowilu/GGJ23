using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WaveManager waveManager;
    private async void Start()
    {
        await new WaitForSeconds(2f);
        //PowerManager.instance.OfferPowers();
        waveManager.StartNewWave();
    }
}
