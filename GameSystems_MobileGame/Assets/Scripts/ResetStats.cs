using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetStats : MonoBehaviour
{
    public Slider[] sliders;
    public Image[] fill;
    public Color black;

    public void ResetSliders()
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].value = 0;
            fill[i].color = Color.black;
        }
    }
}
