using TMPro;
using UnityEngine;

public class Note : MonoBehaviour, IInteractable
{
    [Header("UI")]
    public GameObject noteUI;
    public TextMeshProUGUI noteText; // текст внутри UI

    [Header("Data")]
    public EvidenceData evidence;

    private bool isOpen = false;
    private bool isCollected = false;

    public static bool isUIOpen = false;

    private EvidenceSystem evidenceSystem;

    void Start()
    {
        evidenceSystem = FindObjectOfType<EvidenceSystem>();

        if (evidenceSystem == null)
        {
            Debug.LogError("EvidenceSystem NOT FOUND on scene!");
        }

        if (noteUI != null)
        {
            noteUI.SetActive(false);
        }
    }

    public string GetPromptText()
    {
        return "[E] Читать";
    }

    public void Interact()
    {
        if (!isOpen)
        {
            OpenNote();
        }
    }

    void Update()
    {
        if (!isOpen) return;

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
        {
            CloseNote();
        }
    }

    void OpenNote()
    {
        if (noteUI == null)
        {
            Debug.LogError("Note UI is not assigned!");
            return;
        }

        noteUI.SetActive(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        isOpen = true;
        isUIOpen = true;

        // 🔥 скрываем задачи
        if (ObjectiveSystem.instance != null)
            ObjectiveSystem.instance.ShowObjectives(false);

        // 🔥 подставляем текст из EvidenceData
        if (noteText != null && evidence != null)
        {
            noteText.text = evidence.description;
        }
    }

    void CloseNote()
    {
        if (noteUI != null)
            noteUI.SetActive(false);

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isOpen = false;
        isUIOpen = false;

        // 🔥 возвращаем задачи
        if (ObjectiveSystem.instance != null)
            ObjectiveSystem.instance.ShowObjectives(true);

        // 🔥 добавляем улику только 1 раз
        if (!isCollected)
        {
            isCollected = true;
            if (evidenceSystem != null && evidence != null)
            {

                evidenceSystem.AddEvidence(evidence);

            }

            Destroy(gameObject);
        }
    }
}