using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GoalVilla : MonoBehaviour
{
    public static GoalVilla instance;

    public TextMeshProUGUI objectiveText;

    private List<string> objectives = new List<string>();
    private List<bool> completed = new List<bool>();

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        AddObjective("Исследовать виллу");
        AddObjective("Найти секретную дверь");
        AddObjective("Взломать секретную дверь");
        AddObjective("Найти ключ");
        AddObjective("Найти дверь в подвал");
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
    }

    void UpdateUI()
    {
        string result = "Задачи:\n";

        for (int i = 0; i < objectives.Count; i++)
        {
            if (completed[i])
            {
                result += $"<s>• {objectives[i]}</s>\n";
            }
            else
            {
                result += $"• {objectives[i]}\n";
            }
        }

        objectiveText.text = result;
    }

    public void ShowObjectives(bool show)
    {
        objectiveText.gameObject.SetActive(show);
    }
}