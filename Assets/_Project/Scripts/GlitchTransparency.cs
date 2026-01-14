using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlitchTransparency : MonoBehaviour
{
    [Range(0f, 1f)]
    public float minAlpha = 0.2f;

    [Range(0f, 1f)]
    public float maxAlpha = 1f;

    public float strongChangeSpeed = 3f;
    public float weakChangeSpeed = 0.6f;
    public float pauseBetweenGlitches = 0.15f;

    private List<Image> childImages = new List<Image>();

    void Awake()
    {
        // Собираем только дочерние Image, исключая Image на самом объекте
        var all = GetComponentsInChildren<Image>(includeInactive: true);
        var selfImage = GetComponent<Image>();

        foreach (var img in all)
        {
            if (img == selfImage) continue;
            childImages.Add(img);
        }
    }

    void OnEnable()
    {
        if (childImages.Count > 0)
            StartCoroutine(GlitchLoop());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator GlitchLoop()
    {
        while (true)
        {
            // выбираем один "главный" Image для сильного глитча
            int strongIndex = Random.Range(0, childImages.Count);
            float targetStrong = Random.Range(minAlpha, maxAlpha);
            float targetWeak = Random.Range(minAlpha, maxAlpha);

            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime;

                for (int i = 0; i < childImages.Count; i++)
                {
                    var img = childImages[i];
                    if (img == null) continue;

                    var c = img.color;

                    if (i == strongIndex)
                    {
                        // сильное изменение
                        float a = Mathf.Lerp(c.a, targetStrong, strongChangeSpeed * Time.deltaTime);
                        c.a = a;
                    }
                    else
                    {
                        // слабое колебание
                        float a = Mathf.Lerp(c.a, targetWeak, weakChangeSpeed * Time.deltaTime);
                        c.a = a;
                    }

                    img.color = c;
                }

                yield return null;
            }

            yield return new WaitForSeconds(pauseBetweenGlitches);
        }
    }
}
