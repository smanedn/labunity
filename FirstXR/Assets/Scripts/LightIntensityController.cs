using UnityEngine;
using UnityEngine.UI;

public class LightIntensityController : MonoBehaviour
{
    public Light directionalLight;

    public Slider intensitySlider;

    void Start()
    {
        if (directionalLight == null)
        {
            return;
        }

        if (intensitySlider == null)
        {
            return;
        }

        intensitySlider.value = directionalLight.intensity;

        intensitySlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void OnSliderValueChanged(float value)
    {
        if (directionalLight != null)
        {
            directionalLight.intensity = value;
        }
    }
}
