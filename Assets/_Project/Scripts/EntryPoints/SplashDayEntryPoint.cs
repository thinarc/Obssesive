using System;
using DG.Tweening;
using UnityEngine;

public class SplashDayEntryPoint : MonoBehaviour
{
    public float timer = 10f;
    public CanvasGroup canvasGroup31;
    public CanvasGroup canvasGroup2;
    
    public TranslaterAction translaterAction;
    public GameObject next;

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = 100000f;
            canvasGroup31.DOFade(0f, 1f).SetEase(Ease.InElastic);
            canvasGroup2.DOFade(1f, 1.2f).SetEase(Ease.InElastic).OnComplete(() =>
            {
                translaterAction.TScene(next);
            });
        }
    }
}
