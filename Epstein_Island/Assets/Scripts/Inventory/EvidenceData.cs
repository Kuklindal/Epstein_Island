using UnityEngine;

public enum EvidenceType
{
    Main,       // ключевая
    Secondary   // второстепенная
}

[CreateAssetMenu(fileName = "Evidence", menuName = "Game/Evidence")]
public class EvidenceData : ScriptableObject
{
    public string evidenceID;
    public string evidenceName;
    public EvidenceType type;
}