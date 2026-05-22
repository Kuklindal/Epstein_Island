using TMPro;
using UnityEngine;

public class Note : MonoBehaviour, IInteractable
{
    [Header("UI")]
    public GameObject noteUI;
    public TextMeshProUGUI noteText;

    [Header("Data")]
    public EvidenceData evidence;

    private bool isOpen = false;
    private bool isCollected = false;

    public static bool isUIOpen = false;

    private EvidenceSystem evidenceSystem;

    void Start()
    {
        evidenceSystem = FindObjectOfType<EvidenceSystem>();

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

        if (Input.GetKeyDown(KeyCode.E) ||
            Input.GetKeyDown(KeyCode.Escape))
        {
            CloseNote();
        }
    }

    void OpenNote()
    {
        noteUI.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        isOpen = true;
        isUIOpen = true;

        if (ObjectiveSystem.instance != null)
            ObjectiveSystem.instance.ShowObjectives(false);

        if (noteText != null && evidence != null)
        {
            noteText.text = evidence.description;
        }
    }

    void CloseNote()
    {
        // СНАЧАЛА закрываем UI
        noteUI.SetActive(false);

        // ВОЗВРАЩАЕМ игру
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isOpen = false;
        isUIOpen = false;

        if (ObjectiveSystem.instance != null)
            ObjectiveSystem.instance.ShowObjectives(true);

        // добавляем улику
        if (!isCollected)
        {
            isCollected = true;

            if (evidenceSystem != null && evidence != null)
            {
                evidenceSystem.AddEvidence(evidence);
            }
        }

        // УДАЛЯЕМ записку из мира
        gameObject.SetActive(false);
    }
}