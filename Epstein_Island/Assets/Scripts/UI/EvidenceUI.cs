using TMPro;
using UnityEngine;

public class EvidenceUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI text;
    public GameObject tutorialUI;
    private EvidenceSystem evidenceSystem;
    private bool isOpen = false;
    public static bool isEvidenceOpen = false;
    void Start()
    {
        evidenceSystem = FindObjectOfType<EvidenceSystem>();
        panel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Toggle();
        }
    }

    void Toggle()
    {
        isOpen = !isOpen;
        isEvidenceOpen = isOpen;
        panel.SetActive(isOpen);

        if (isOpen)
        {
            UpdateUI();

            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (ObjectiveSystem.instance != null)
                ObjectiveSystem.instance.ShowObjectives(false);

            if (tutorialUI != null)
                tutorialUI.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (ObjectiveSystem.instance != null)
                ObjectiveSystem.instance.ShowObjectives(true);

            if (tutorialUI != null)
                tutorialUI.SetActive(true);
        }
    }

    public void UpdateUI()
    {
        if (evidenceSystem == null)
        {
            text.text = "Система улик не найдена.";
            return;
        }

        if (evidenceSystem.collectedEvidence.Count == 0)
        {
            text.text = "Пока ничего не найдено.";
            return;
        }

        text.text = "";

        foreach (var ev in evidenceSystem.collectedEvidence)
        {
            text.text += "• " + ev.evidenceName + "\n";
        }
    }
}