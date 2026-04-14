using System.Collections.Generic;
using UnityEngine;

public class EvidenceSystem : MonoBehaviour
{
    public List<EvidenceData> collectedEvidence = new List<EvidenceData>();

    public int requiredEvidence = 1;

    public void AddEvidence(EvidenceData evidence)
    {
        Debug.Log("AddEvidence called");
        if (evidence == null)
        {
            Debug.LogError("Evidence is NULL!");
            return;
        }

        if (!collectedEvidence.Contains(evidence))
        {
            collectedEvidence.Add(evidence);

            Debug.Log("Evidence collected: " + evidence.evidenceName);

            // 🔥 УВЕДОМЛЕНИЕ
            UIManager.instance.ShowMessage("Улика добавлена: " + evidence.evidenceName);

            // 🔥 ОБНОВЛЯЕМ UI СПИСКА УЛИК
            EvidenceUI ui = FindObjectOfType<EvidenceUI>();
            if (ui != null)
            {
                ui.UpdateUI();
            }
            ObjectiveSystem.instance.CompleteObjective("Прочитать записку");

        }
        else { Debug.Log("треш"); }
    }
}