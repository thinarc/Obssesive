using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace _Project.Scripts.Services
{
    public class Terminal2 : MonoBehaviour
    {
        [SerializeField] public Button button;

        public async void Initialize(bool cle, Action callback)
        {
            this.cle = cle;
            button.onClick.AddListener((() => callback()));
            button.onClick.AddListener(TriggerAnim);
            if (cle == false) button.onClick.AddListener((() =>
            {
                cl = true;
            }));
            else button.gameObject.SetActive(true);
            var startScale = transform.localScale;
            transform.localScale = Vector3.zero;
            transform.DOScale(startScale, 0.2f).SetEase(Ease.InQuad);
            // SoundManager.Instance?.PlayOneShot(SoundManager.Instance?.tone);
            
            await UniTask.Delay(TimeSpan.FromSeconds(1.8f));
            TriggerAnim();
        }

        public void TriggerAnim()
        {
            // transform.DOScale(0, 0.2f).SetEase(Ease.Flash);
            transform.DOMove(transform.position + new Vector3(0, -2, 0), 1f).SetEase(Ease.InQuad);
            GetComponent<CanvasGroup>().DOFade(0, 0.2f).SetEase(Ease.Flash);
            // SoundManager.Instance?.PlayOneShot(SoundManager.Instance?.random);
        }

        private void OnDisable()
        {
            button?.onClick?.RemoveAllListeners();
        }

        private bool cl;
        private bool cle;
        public void Update()
        {
            if (UnityEngine.Input.GetMouseButton(0) && !cl && !cle)
            {
                button.onClick.Invoke();
                cl = true;
            }
        }
    }
}
