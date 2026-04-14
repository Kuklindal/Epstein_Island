using UnityEngine;

public enum EvidenceType
{
    Main,
    Secondary
}

[CreateAssetMenu(fileName = "Evidence", menuName = "Game/Evidence")]
public class EvidenceData : ScriptableObject
{
    public string evidenceID;
    public string evidenceName;
    public EvidenceType type;

    [TextArea]
    public string description; // 🔥 добавь это
}