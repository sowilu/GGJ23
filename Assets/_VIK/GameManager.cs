using UnityEngine;

public class GameManager : MonoBehaviour
{
    private async void Start()
    {
        await new WaitForSeconds(2f);
        PowerManager.instance.OfferPowers();
    }
}
