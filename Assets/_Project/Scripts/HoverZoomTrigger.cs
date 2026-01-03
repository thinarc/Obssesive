using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverZoomTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Список изображений для зума")]
    public List<RectTransform> targetImages;

    [Header("Параметры зума")]
    public float zoomScale = 1.15f;
    public float animationDuration = 0.15f;

    // внутреннее хранилище исходных масштабов
    private Dictionary<RectTransform, Vector3> originalScales = new Dictionary<RectTransform, Vector3>();
    private Dictionary<RectTransform, Coroutine> runningCoroutines = new Dictionary<RectTransform, Coroutine>();

    private void Awake()
    {
        foreach (var rt in targetImages)
        {
            if (rt == null) continue;
            originalScales[rt] = rt.localScale;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (var rt in targetImages)
        {
            if (rt == null) continue;
            StartScale(rt, originalScales[rt] * zoomScale);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (var rt in targetImages)
        {
            if (rt == null) continue;
            StartScale(rt, originalScales[rt]);
        }
    }

    private void StartScale(RectTransform rt, Vector3 targetScale)
    {
        if (runningCoroutines.ContainsKey(rt) && runningCoroutines[rt] != null)
            StopCoroutine(runningCoroutines[rt]);

        runningCoroutines[rt] = StartCoroutine(ScaleRoutine(rt, targetScale));
    }

    private IEnumerator ScaleRoutine(RectTransform rt, Vector3 targetScale)
    {
        Vector3 start = rt.localScale;
        float t = 0f;

        while (t < animationDuration)
        {
            t += Time.unscaledDeltaTime;
            float k = t / animationDuration;
            rt.localScale = Vector3.Lerp(start, targetScale, k);
            yield return null;
        }

        rt.localScale = targetScale;
    }
}
