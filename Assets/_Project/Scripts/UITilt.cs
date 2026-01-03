using System;
using UnityEngine;

public class UITilt : MonoBehaviour
{
    public RectTransform target;
    public float tilt = 10f;

  public void Start()
    {
        if (target == null)
        {
            target = GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        if (target == null) Start();
        
        Vector2 mouse = Input.mousePosition;
        Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector2 dir = (mouse - center) / Screen.width;

        float rotX =  dir.y * tilt;
        float rotY = -dir.x * tilt;

        target.localRotation = Quaternion.Euler(rotX, rotY, 0f);
        target.localPosition = new Vector3(rotY, -rotX, 0f);
    }
}