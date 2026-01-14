using System;
using UnityEngine;

public class TranslaterAction : MonoBehaviour
{
    public GameObject current;
    public bool first;

    public void Start()
    {
        if (!first) current.SetActive(false);
        else current.SetActive(true);
    }

    public void TScene(GameObject next)
    {
        next.SetActive(true);
        current.SetActive(false);
    }
}
