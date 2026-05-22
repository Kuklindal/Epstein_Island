using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveSystem : MonoBehaviour
{
    public static ObjectiveSystem instance;

    public TextMeshProUGUI objectiveText;

    private List<string> objectives = new List<string>();
    private List<bool> completed = new List<bool>();


    void Awake()
    {
        instance = this;
        ObjectiveSystem.instance.AddObjective("Найти записку");      // 0
        ObjectiveSystem.instance.AddObjective("Прочитать записку"); // 1
        ObjectiveSystem.instance.AddObjective("Попасть в виллу");
    }

    public void AddObjective(string text)
    {
        objectives.Add(text);
        completed.Add(false);
        UpdateUI();
    }

    public void CompleteObjective(string name)
    {
        for (int i = 0; i < objectives.Count; i++)
        {
            if (objectives[i] == name)
            {
                completed[i] = true;
                Debug.Log("Objective completed: " + name);
                UpdateUI();
                return;
            }
        }
        Debug.LogError("OBJECTIVE NOT FOUND: " + name);
    }
    void UpdateUI()
    {
        string result = "Задачи:\n";

        for (int i = 0; i < objectives.Count; i++)
        {
            if (completed[i])
            {
                result += $"<s>{objectives[i]}</s>\n";
            }
            else
            {
                result += $"{objectives[i]}\n";
            }
        }

        objectiveText.text = result;
    }
    public void ShowObjectives(bool show)
    {
        objectiveText.gameObject.SetActive(show);
    }
}