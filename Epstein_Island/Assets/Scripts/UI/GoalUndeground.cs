using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalUndeground : MonoBehaviour
{
    public static GoalUndeground instance;

    public TextMeshProUGUI objectiveText;

    private List<string> objectives = new List<string>();
    private List<bool> completed = new List<bool>();
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        AddObjective("Включить генератор");
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
                switch (name)
                {
                    case "Включить генератор":
                        AddObjective("Найти морг");
                        AddObjective("Прочитать записку");
                        return;
                    case "Прочитать записку":
                        AddObjective("Прослушать диктофон");
                        return;
                    case "Прослушать диктофон":
                        AddObjective("Найти и открыть вентиляцию");
                        return;
                    case "Найти и открыть вентиляцию":
                        AddObjective("Взять ключ");
                        return;
                    case "Взять ключ":
                        AddObjective("Найти архив");
                        return;
                }
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
