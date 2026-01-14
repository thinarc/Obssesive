using System;
using _Project.Scripts.Services;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using UnityEngine;

namespace _Project.Scripts.EntryPoints
{
    public class ClickEnter : MonoBehaviour
    {
        public GameObject can;
        public TerminalPool2 terminalPool2;

        public Slider opSlider;
        public float opValue = 0;
        public float opMax;

        public float adder = 1;
        public GameObject effectAdder;
        public Transform butt;
        public Canvas canvas;
        
        public TextMeshProUGUI opNotif;
        
        public void Start()
        {
            can.SetActive(false);
            
            opSlider.maxValue = opMax;
        }

        public async void Initialize()
        {
            can.SetActive(true);
            await terminalPool2.CreateTerminalAsync(true, "Your operation button");
            terminalPool2.CreateTerminal(true, "Click it to proceed");
        }

        public bool skril;
        public void OpAdd()
        {
            opValue += adder;
            opSlider.value = opValue;

            if (opValue > 7 && !skril)
            {
                opNotif.DOFade(0, 0.35f).SetEase(Ease.Flash);
                skril = true;
            }

            var effect = Instantiate(effectAdder, butt, true);
            
            var parent = (RectTransform)butt;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parent,
                UnityEngine.Input.mousePosition,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
                out Vector2 localPoint
            );
            effect.transform.localPosition = localPoint;
            effect.transform.localScale *= UnityEngine.Random.Range(0.8f, 1.5f);

            effect.transform.DOMove(effect.transform.position + new Vector3(1 * UnityEngine.Random.Range(-1f, 1f), 1.5f * UnityEngine.Random.Range(0f, 1.35f), 0), 0.35f).SetEase(Ease.Flash);
            effect.GetComponent<CanvasGroup>().DOFade(0, 0.35f).SetEase(Ease.Flash);
            effect.transform.DOScale(0.35f, 0.35f).SetEase(Ease.Flash);
        }
    }
}