using UnityEngine;
using UnityEngine.UI;

public class EvidenceViewer : MonoBehaviour
{
    public static EvidenceViewer instance;

    public GameObject panel;
    public Image evidenceImage;

    private void Awake()
    {
        instance = this;
    }

    public void ShowEvidence(Sprite sprite)
    {
        panel.SetActive(true);

        evidenceImage.sprite = sprite;

        Time.timeScale = 0f;
    }

    void Update()
    {
        if (panel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(false);

            Time.timeScale = 1f;
        }
    }
}