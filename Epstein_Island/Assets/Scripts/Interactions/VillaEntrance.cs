using UnityEngine;
using UnityEngine.SceneManagement;

public class VillaEntrance : MonoBehaviour, IInteractable
{
    public string sceneName = "Villa";

    public string GetPromptText()
    {
        return "[E] Войти в виллу";
    }

    public void Interact()
    {
        Debug.Log("Loading Villa scene...");

        SceneManager.LoadScene(sceneName);
    }
}