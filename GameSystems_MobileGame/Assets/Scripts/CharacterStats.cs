using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public Image [] slider;

    public void Reset()
    {
        slider[0].color = new Color32(0, 0, 0, 225);
        slider[1].color = new Color32(0, 0, 0, 225);
        slider[2].color = new Color32(0, 0, 0, 225);
    }

    public void Triangle()
    {
        slider[0].color = new Color32(255, 255, 0, 225);
        slider[1].color = new Color32(255, 255, 0, 225);
        slider[2].color = new Color32(255, 255, 0, 225);
    }

    public void Circle()
    {
        slider[0].color = new Color32(0, 255, 0, 225);
        slider[1].color = new Color32(0, 255, 0, 225);
        slider[2].color = new Color32(0, 255, 0, 225);
    }

    public void Diamond()
    {
        slider[0].color = new Color32(0, 0, 255, 225);
        slider[1].color = new Color32(0, 0, 255, 225);
        slider[2].color = new Color32(0, 0, 255, 225);
    }

    public void Square()
    {
        slider[0].color = new Color32(255, 0, 0, 225);
        slider[1].color = new Color32(255, 0, 0, 225);
        slider[2].color = new Color32(255, 0, 0, 225);
    }
}
