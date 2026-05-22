using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    [Header("UI")]
    public Slider volumeSlider;
    public Slider brightnessSlider;

    [Header("Brightness")]
    public Image brightnessOverlay;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        // Загружаем сохраненные настройки
        float volume = PlayerPrefs.GetFloat("Volume", 1f);
        float brightness = PlayerPrefs.GetFloat("Brightness", 1f);

        volumeSlider.value = volume;
        brightnessSlider.value = brightness;

        SetVolume(volume);
        SetBrightness(brightness);

        // Подписка на изменение
        volumeSlider.onValueChanged.AddListener(SetVolume);
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;

        PlayerPrefs.SetFloat("Volume", value);
    }

    public void SetBrightness(float value)
    {
        Color color = brightnessOverlay.color;

        // Чем меньше brightness — тем темнее экран
        color.a = 1f - value;

        brightnessOverlay.color = color;

        PlayerPrefs.SetFloat("Brightness", value);
    }
}