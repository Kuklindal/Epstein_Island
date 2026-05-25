using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class FinalSceneController : MonoBehaviour
{
    [Header("Player")]
    public Transform player;
    public CharacterController controller;
    public float playerRunSpeed = 10f;

    [Header("Route")]
    public Transform boatTarget;
    public float finishDistance = 2f;

    [Header("Camera")]
    public Camera playerCamera;
    public Transform enemyLookPoint;
    public float lookBackDistance = 50f;
    public float lookBackSpeed = 2.5f;
    public float lookBackHoldTime = 1.2f;

    [Header("Enemy")]
    public EnemyRun enemyRun;

    [Header("UI")]
    public CanvasGroup fadeCanvas;
    public TextMeshProUGUI endText;
    public float fadeSpeed = 1.5f;

    [Header("Scene")]
    public string finalSceneName = "";
    public bool loadAnotherScene = false;

    private bool hasLookedBack = false;

    void Start()
    {
        if (endText != null)
            endText.gameObject.SetActive(false);

        if (fadeCanvas != null)
            fadeCanvas.alpha = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(FinalSequence());
    }

    IEnumerator FinalSequence()
    {
        yield return StartCoroutine(FadeIn());

        yield return new WaitForSeconds(0.5f);

        while (Vector3.Distance(player.position, boatTarget.position) > finishDistance)
        {
            MovePlayerToBoat();

            float distanceToBoat = Vector3.Distance(player.position, boatTarget.position);

            if (!hasLookedBack && distanceToBoat <= lookBackDistance)
            {
                hasLookedBack = true;

                if (enemyRun != null)
                    enemyRun.StartChase();

                yield return StartCoroutine(LookBackSequence());
            }

            yield return null;
        }

        if (enemyRun != null)
            enemyRun.StopChase();

        yield return StartCoroutine(FadeOut());

        if (endText != null)
            endText.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (loadAnotherScene && !string.IsNullOrEmpty(finalSceneName))
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(finalSceneName);
        }
    }

    void MovePlayerToBoat()
    {
        Vector3 dir = boatTarget.position - player.position;
        dir.y = 0f;
        dir.Normalize();

        if (controller != null)
        {
            controller.Move(dir * playerRunSpeed * Time.deltaTime);
        }
        else
        {
            player.position += dir * playerRunSpeed * Time.deltaTime;
        }

        if (dir != Vector3.zero)
        {
            player.rotation = Quaternion.Slerp(
                player.rotation,
                Quaternion.LookRotation(dir),
                Time.deltaTime * 6f
            );
        }
    }

    IEnumerator LookBackSequence()
    {
        if (playerCamera == null || enemyLookPoint == null)
            yield break;

        Quaternion startRotation = playerCamera.transform.rotation;

        Vector3 lookDir = enemyLookPoint.position - playerCamera.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(lookDir);

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * lookBackSpeed;
            playerCamera.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        yield return new WaitForSeconds(lookBackHoldTime);

        t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * lookBackSpeed;
            playerCamera.transform.rotation = Quaternion.Slerp(targetRotation, startRotation, t);
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        if (fadeCanvas == null)
            yield break;

        while (fadeCanvas.alpha > 0f)
        {
            fadeCanvas.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }

        fadeCanvas.alpha = 0f;
    }

    IEnumerator FadeOut()
    {
        if (fadeCanvas == null)
            yield break;

        while (fadeCanvas.alpha < 1f)
        {
            fadeCanvas.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        fadeCanvas.alpha = 1f;
    }
}