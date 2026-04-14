using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI notificationText;

    private float timer;

    void Awake()
    {
        instance = this;
        notificationText.gameObject.SetActive(false);
    }

    public void ShowMessage(string message)
    {
        notificationText.text = message;
        notificationText.gameObject.SetActive(true);
        timer = 2f;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.unscaledDeltaTime;

            if (timer <= 0)
            {
                notificationText.gameObject.SetActive(false);
            }
        }
    }
}