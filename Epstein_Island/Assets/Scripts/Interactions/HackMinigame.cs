using UnityEngine;
using UnityEngine.UI;

public class HackMinigame : MonoBehaviour
{
    public RectTransform slider;
    public RectTransform targetZone;

    public float speed = 1f;
    public int totalStages = 3;

    private RectTransform bar;

    private bool isPlaying;
    private int currentStage;
    private float sliderPos;
    private int direction = 1;

    private float targetCenter;
    private float targetSize;

    private float inputDelay;

    public System.Action onSuccess;

    private readonly float[] stageSizes = { 0.30f, 0.18f, 0.10f };

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip tickSound;
    public AudioClip successSound;
    public AudioClip failSound;

    [Header("Visual")]
    public Image targetImage;

    public void StartGame()
    {
        bar = slider.parent.GetComponent<RectTransform>();

        gameObject.SetActive(true);

        currentStage = 0;
        sliderPos = 0f;
        direction = 1;
        isPlaying = true;
        inputDelay = 0.2f;

        SetupStage();
        UpdateUI();
    }

    void Update()
    {
        if (!isPlaying) return;

        if (inputDelay > 0f)
        {
            inputDelay -= Time.unscaledDeltaTime;
            MoveSlider();
            return;
        }

        MoveSlider();

        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckHit();
        }
    }

    void MoveSlider()
    {
        sliderPos += speed * direction * Time.unscaledDeltaTime;

        if (sliderPos >= 1f)
        {
            sliderPos = 1f;
            direction = -1;
        }

        if (sliderPos <= 0f)
        {
            sliderPos = 0f;
            direction = 1;
        }

        UpdateUI();

        // 🔊 редкий тик (напряжение)
        if (audioSource != null && tickSound != null && Random.value < 0.02f)
        {
            audioSource.PlayOneShot(tickSound);
        }
    }

    void UpdateUI()
    {
        float width = bar.rect.width;

        float sliderX = Mathf.Lerp(-width / 2f, width / 2f, sliderPos);
        slider.anchoredPosition = new Vector2(sliderX, slider.anchoredPosition.y);

        float targetX = Mathf.Lerp(-width / 2f, width / 2f, targetCenter);
        float targetWidth = width * targetSize;

        targetZone.anchoredPosition = new Vector2(targetX, targetZone.anchoredPosition.y);
        targetZone.sizeDelta = new Vector2(targetWidth, targetZone.sizeDelta.y);
    }

    void CheckHit()
    {
        float left = targetCenter - targetSize / 2f;
        float right = targetCenter + targetSize / 2f;

        Debug.Log($"Stage {currentStage + 1}: slider={sliderPos}, target={left}-{right}");

        if (sliderPos >= left && sliderPos <= right)
        {
            NextStage();
        }
        else
        {
            Fail();
        }
    }

    void NextStage()
    {
        // 🔊 звук успеха
        if (audioSource != null && successSound != null)
            audioSource.PlayOneShot(successSound);

        // 🎨 зелёный фидбек
        if (targetImage != null)
            targetImage.color = Color.green;

        // ⚡ напряжение
        speed += 0.2f;

        currentStage++;

        if (currentStage >= totalStages)
        {
            Complete();
            return;
        }

        sliderPos = 0f;
        direction = 1;
        inputDelay = 0.15f;

        SetupStage();
        UpdateUI();
    }

    void SetupStage()
    {
        int index = Mathf.Clamp(currentStage, 0, stageSizes.Length - 1);

        targetSize = stageSizes[index];

        float min = targetSize / 2f;
        float max = 1f - targetSize / 2f;

        targetCenter = Random.Range(min, max);

        // 🎨 сброс цвета
        if (targetImage != null)
            targetImage.color = Color.white;
    }

    void Complete()
    {
        Debug.Log("HACK COMPLETE");

        isPlaying = false;
        gameObject.SetActive(false);

        onSuccess?.Invoke();
    }

    void Fail()
    {
        Debug.Log("HACK FAILED");

        // 🔊 звук ошибки
        if (audioSource != null && failSound != null)
            audioSource.PlayOneShot(failSound);

        // 🎨 красный фидбек
        if (targetImage != null)
            targetImage.color = Color.red;

        isPlaying = false;
        currentStage = 0;
        gameObject.SetActive(false);
    }
}