using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public string currentObjective;
    public Text objectiveText;

    public void SetObjective(string newObjective)
    {
    currentObjective = newObjective;

    objectiveText.text = currentObjective;

    Debug.Log("New Objective: " + currentObjective);
    }
}