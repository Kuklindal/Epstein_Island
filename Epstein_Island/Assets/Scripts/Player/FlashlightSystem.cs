using UnityEngine;

public class FlashlightSystem : MonoBehaviour
{
    public Light flashlight;

    public AudioSource audioSource;
    public AudioClip toggleSound;
    public float maxIntensity = 8f;
    public float minIntensity = 2f;
    private bool isOn = true;
    public bool IsOn => isOn;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }

        AdjustIntensity();
    }

    void ToggleFlashlight()
    {
        isOn = !isOn;

        flashlight.enabled = isOn;
        audioSource.PlayOneShot(toggleSound);
    }

    void AdjustIntensity()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        flashlight.intensity += scroll * 5f;

        flashlight.intensity = Mathf.Clamp(flashlight.intensity, minIntensity, maxIntensity);
    }
}