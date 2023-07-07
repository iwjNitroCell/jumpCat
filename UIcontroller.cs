using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    private RectTransform rt;
    public float maxsize;
    public float A;

    public float posX = 0;
    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        rt.anchoredPosition = new Vector2(posX, rt.anchoredPosition.y);
    }
}
