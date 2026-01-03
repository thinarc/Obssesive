using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.EntryPoints
{
    public class LogoEntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject[] logoImages;
        [SerializeField] private float displaySwitch = 0.5f;
        private float _displaySwitch;
        public float randss;

        private int i;
        private float timer;

        private bool _start;

        private void Start()
        {
            foreach (var go in logoImages)
            {
                go.SetActive(false);
                go.GetComponent<UITilt>().Start();
            }
            
            _displaySwitch = displaySwitch;
        }
        
        private void Update()
        {
            if (!_start)
            {
                Start();
                _start = true;
            }
            
            _displaySwitch = displaySwitch;
            _displaySwitch *= Random.Range(1f, randss);
            
            timer += Time.deltaTime;
            if (timer >= _displaySwitch)
            {
                timer = 0f;
                logoImages[i].SetActive(false);
                i++;
                if (i >= logoImages.Length) i = 0;
                logoImages[i].SetActive(true);
            }
        }
    }
}
