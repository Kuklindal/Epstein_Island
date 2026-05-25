using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NPCFinalEvent : MonoBehaviour, IEventTrigger
{
    [Header("Animator")]
    public Animator animator;

    [Header("Head Bone")]
    public Transform head;

    [Header("Fade")]
    public CanvasGroup fadeCanvas;

    [Header("Scene")]
    public string finalSceneName = "FinalScene";

    public void TriggerEvent()
    {
        StartCoroutine(EventRoutine());
    }

IEnumerator EventRoutine()
{
    animator.SetBool("StopDance", true);

    // ждём переход в idle
    yield return new WaitForSeconds(1.5f);

    // выключаем animator
    animator.enabled = false;

    // крутим голову
    if (head != null)
    {
        Quaternion startRot = head.localRotation;
        Quaternion targetRot = Quaternion.Euler(0, 180, 0);

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * 0.7f;

            head.localRotation = Quaternion.Slerp(startRot, targetRot, t);

            yield return null;
        }
    }

    yield return new WaitForSeconds(2f);

    yield return StartCoroutine(FadeToBlack());

    SceneManager.LoadScene(finalSceneName);
}

IEnumerator TurnHead()
{
    Quaternion startRot = head.localRotation;
    Quaternion targetRot = Quaternion.Euler(0, 180, 0);

    float t = 0;

    while (t < 1)
    {
        t += Time.deltaTime * 2f;

        head.localRotation = Quaternion.Slerp(startRot, targetRot, t);

        yield return null;
    }
}

    IEnumerator FadeToBlack()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime;

            fadeCanvas.alpha = t;

            yield return null;
        }
    }
}