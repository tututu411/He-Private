using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeWeatherTab : MonoBehaviour
{
    private TextMeshProUGUI text;
    Color SelectedColor;
    Color UnSelectedColor;
    Junc_Button button;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        ColorUtility.TryParseHtmlString("#B6DDF4", out SelectedColor);
        ColorUtility.TryParseHtmlString("#7F96A9", out UnSelectedColor);
        button = GetComponentInParent<Junc_Button>();
        button.OnSelectedEvent.AddListener(delegate { SetState(true); });
        button.UnSelectedEvent.AddListener(delegate { SetState(false); });
    }

    public void SetState(bool status)
    {
        text.color = status ? SelectedColor : UnSelectedColor;
        if (status)
            text.fontMaterial.EnableKeyword("GLOW_ON");
        else
            text.fontMaterial.DisableKeyword("GLOW_ON");
    }
}
