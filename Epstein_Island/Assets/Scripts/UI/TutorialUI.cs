using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float showTime = 40f;

    private float timer;
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        text.text =
    "[WASD] Ч ƒвижение\n" +
    "[Mouse] Ч ќсмотр\n" +
    "[F] Ч ‘онарик\n" +
    "[Scroll] Ч яркость фонарика\n" +
    "[E] Ч ¬заимодействие";

        timer = showTime;
        canvasGroup.alpha = 1;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            canvasGroup.alpha -= Time.deltaTime;

            if (canvasGroup.alpha <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}