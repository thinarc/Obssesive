using System;
using _Project.Scripts.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.EntryPoints
{
    public class Game1EntryPoint : MonoBehaviour
    {
        public float timer = 2f;

        public GameObject can;
    
        public TranslaterAction translaterAction;
        public GameObject next;

        public static int propellers;
        
        [SerializeField] private TerminalPool terminalPool;

        [SerializeField] private ClickEnter clickEnter;

        private void Awake()
        {
            propellers = 0;
            can.SetActive(true);
        }

        public async void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer = 100000f;
                //canvasGroup31.DOFade(0f, 1f).SetEase(Ease.InElastic);
                //canvasGroup2.DOFade(1f, 1.2f).SetEase(Ease.InElastic).OnComplete(() =>
                //{
                // translaterAction.TScene(next);
                //});

                terminalPool.CreateTerminal(true, "I want to clear it");
                await UniTask.Delay(600);
                terminalPool.CreateTerminal(false, "click them");
            }

            if (propellers >= 26)
            {
                propellers = -1000;
                await terminalPool.CreateTerminalAsync(true, "Good job!");
                
                clickEnter.Initialize();
            }
        }
    }
}