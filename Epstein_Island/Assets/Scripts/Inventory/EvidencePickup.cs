using UnityEngine;

public class EvidencePickup : MonoBehaviour, IInteractable
{
    public EvidenceData evidence;
    public MonoBehaviour eventTrigger;
    private EvidenceSystem evidenceSystem;
    private EvidenceViewer viewer;

    void Start()
    {
        evidenceSystem = FindObjectOfType<EvidenceSystem>();
        viewer = FindObjectOfType<EvidenceViewer>();
    }

    public void Interact()
    {
        if (evidence == null)
            return;

        // добавляем улику
        evidenceSystem.AddEvidence(evidence);

        // показываем документ
        viewer.ShowEvidence(evidence.evidenceImage);

        // удаляем объект со сцены
        Destroy(transform.root.gameObject);
        if (eventTrigger is IEventTrigger trigger)
        {
            trigger.TriggerEvent();
        }
    }

    public string GetPromptText()
    {
        return "[E] Осмотреть улику";
    }
}