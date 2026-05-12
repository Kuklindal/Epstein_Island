using System.Collections;
using UnityEngine;

public class ScareEvent : MonoBehaviour, IEventTrigger
{
    public AudioSource scareSound;

    public Light[] lightsToDisable;

    public Transform[] doorsToOpen;

    public float openAngle = 70f;

    public float doorOpenSpeed = 2f;

    public float delayBeforeScare = 1f;

    private bool activated = false;

    public void TriggerEvent()
    {
        if (activated)
            return;

        activated = true;

        StartCoroutine(ScareSequence());
    }

    IEnumerator ScareSequence()
    {
        // ÇÀÄÅÐÆÊÀ
        yield return new WaitForSeconds(delayBeforeScare);

        // ÇÂÓÊ
        if (scareSound != null)
        {
            scareSound.Play();
        }

        // ÑÂÅÒ
        foreach (Light light in lightsToDisable)
        {
            light.enabled = false;
        }

        // ÄÂÅÐÈ
        StartCoroutine(OpenDoors());
    }

    IEnumerator OpenDoors()
    {
        Quaternion[] startRotations = new Quaternion[doorsToOpen.Length];
        Quaternion[] targetRotations = new Quaternion[doorsToOpen.Length];

        for (int i = 0; i < doorsToOpen.Length; i++)
        {
            startRotations[i] = doorsToOpen[i].localRotation;

            targetRotations[i] =
                Quaternion.Euler(0, openAngle, 0);
        }

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * doorOpenSpeed;

            for (int i = 0; i < doorsToOpen.Length; i++)
            {
                doorsToOpen[i].localRotation =
                    Quaternion.Lerp(
                        startRotations[i],
                        targetRotations[i],
                        t
                    );
            }

            yield return null;
        }
    }
}