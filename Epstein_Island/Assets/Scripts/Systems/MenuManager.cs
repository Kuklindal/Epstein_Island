using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Scenes")]
    public string firstScene = "Prologue_beach";

    [Header("UI")]
    public GameObject settingsPanel;

    public void StartGame()
    {
        Debug.Log("START BUTTON CLICKED");
        SceneManager.LoadScene(firstScene);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    // 🔥 Открыть настройки
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    // 🔥 Закрыть настройки
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
}