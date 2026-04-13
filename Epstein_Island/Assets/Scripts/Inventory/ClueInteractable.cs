using UnityEngine;

public class ClueInteractable : MonoBehaviour
{
    public GameObject clueUI;

    public void Interact()
    {
        Debug.Log("Clue found!");

        if (clueUI != null)
            clueUI.SetActive(true);
    }
}