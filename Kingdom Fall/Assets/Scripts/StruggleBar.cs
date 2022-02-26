using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StruggleBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxStruggle(float maxStruggle)
    {
        slider.maxValue = maxStruggle;
        slider.value = maxStruggle / 2;
    }

    public void Increase(float struggleAmount)
    {
        slider.value += struggleAmount;
    }

    public void Decrease(float decreaseAmount)
    {
        slider.value -= decreaseAmount;
    }
}
