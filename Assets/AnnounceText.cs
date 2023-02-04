using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AnnounceText : MonoBehaviour
{
    TMP_Text text;
    public float duration = 3f;
    public float fadeDuration = 1f;
    public Material material;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        text.material = material;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public async void ShowMessage(string message)
    {
        gameObject.SetActive(true);
        
        text.text = message;
        // fade in 
        material.DOFade(1f, fadeDuration).ChangeStartValue(0f);
        await new WaitForSeconds(duration);
        // fade out
        material.DOFade(0f, fadeDuration);
        
        gameObject.SetActive(false);
    }

}
