using UnityEngine;

public class RecorderPickup : MonoBehaviour, IInteractable
{
    public AudioSource recording;

    private bool pickedUp = false;

    public void Interact()
    {
        if (pickedUp)
            return;
        GoalUndeground.instance.CompleteObjective("Прослушать диктофон");
        pickedUp = true;
        recording.Play();

        Destroy(gameObject);
    }

    public string GetPromptText()
    {
        return "Взять диктофон";
    }
}