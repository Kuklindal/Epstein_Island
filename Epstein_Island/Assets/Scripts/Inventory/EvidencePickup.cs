using UnityEngine;

public class EvidencePickup : MonoBehaviour, IInteractable
{
    public EvidenceData evidence;

    private EvidenceSystem evidenceSystem;

    void Start()
    {
        evidenceSystem = FindObjectOfType<EvidenceSystem>();
    }

    public void Interact()
    {
        evidenceSystem.AddEvidence(evidence);
        Destroy(gameObject);
    }

    public string GetPromptText()
    {
        return "Inspect evidence";
    }
}