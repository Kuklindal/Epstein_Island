using System.Collections;
using UnityEngine;

public class PowerSystem : MonoBehaviour
{
    public AudioSource electricityAudio;
    public Light[] basementLights;

    public void RestorePower()
    {
        StartCoroutine(RestorePowerRoutine());
    }

    IEnumerator RestorePowerRoutine()
    {
        yield return new WaitForSeconds(7f);

        electricityAudio.Play();

        foreach (Light light in basementLights)
        {
            light.enabled = true;
        }
    }
}