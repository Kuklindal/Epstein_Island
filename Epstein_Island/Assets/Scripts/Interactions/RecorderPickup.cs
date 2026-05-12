using UnityEngine;

public class RecorderPickup : MonoBehaviour, IInteractable
{
    public AudioSource recording;

    private bool pickedUp = false;

    public void Interact()
    {
        if (pickedUp)
            return;

        pickedUp = true;

        //// сообщение
        //UIManager.instance.ShowMessage(
        //    "Вы нашли диктофон..."
        //);

        // воспроизводим запись
        recording.Play();

        //// новая цель
        //ObjectiveSystem.instance.SetObjective(
        //    "Найти архив B-12"
        //);

        // убрать объект
        Destroy(gameObject);
    }

    public string GetPromptText()
    {
        return "Взять диктофон";
    }
}