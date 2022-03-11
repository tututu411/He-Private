using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatMapSlider : MonoBehaviour
{

    [SerializeField] private Slider slider;
    [SerializeField] private Image image;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(float arg0)
    {
        if (Math.Abs(arg0 - 0.333f) <= 0.01f || Math.Abs(arg0 - 0.666f) <= 0.01f || Math.Abs(arg0 - 0f) <= 0.01f || Math.Abs(arg0 - 1f) <= 0.01f)
            image.gameObject.SetActive(true);
        else
            image.gameObject.SetActive(false);
    }
}
