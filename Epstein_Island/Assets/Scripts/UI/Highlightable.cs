using UnityEngine;

public class Highlightable : MonoBehaviour
{
    private Material material;

    private Color defaultColor;
    public Color highlightColor = Color.white;

    public float intensity = 3f; // сила подсветки

    void Start()
    {
        material = GetComponent<Renderer>().material;

        // сохраняем исходный цвет
        defaultColor = material.color;
    }

    public void Highlight()
    {
        material.color = highlightColor * intensity;
        ObjectiveSystem.instance.CompleteObjective("Найти записку");
    }

    public void UnHighlight()
    {
        material.color = defaultColor;
    }
}