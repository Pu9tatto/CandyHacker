using UnityEngine;
using UnityEngine.UI;

public class SliderValueChanger : MonoBehaviour
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void ChangeSliderValue(float value)
    {
        _slider.value = value;
    }
}
