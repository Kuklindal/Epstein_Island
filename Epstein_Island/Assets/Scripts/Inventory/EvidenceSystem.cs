using System.Collections.Generic;
using UnityEngine;

public class EvidenceSystem : MonoBehaviour
{
    public List<EvidenceData> collectedEvidence = new List<EvidenceData>();

    private QuestManager questManager;

    public int requiredEvidence = 1;

    void Start()
    {
       questManager = FindObjectOfType<QuestManager>();
    }

    public void AddEvidence(EvidenceData evidence)
    {
    if (!collectedEvidence.Contains(evidence))
    {
        collectedEvidence.Add(evidence);

        Debug.Log("Evidence collected: " + evidence.evidenceName);

        if (evidence.type == EvidenceType.Main)
        {
            questManager.SetObjective("Find more evidence");
        }

        if (collectedEvidence.Count >= requiredEvidence)
        {
            Debug.Log("All evidence collected!");
        }
    }
    } 
}