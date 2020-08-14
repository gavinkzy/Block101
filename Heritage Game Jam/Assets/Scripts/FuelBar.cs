using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxValue(float fuel)
    {
        slider.maxValue = fuel;
        slider.value = fuel;
    }
    public void SetValue(float fuel)
    {
        slider.value = fuel;
    }
}
