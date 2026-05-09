using UnityEngine;

public class KeyPickup : MonoBehaviour, IInteractable
{
    public string keyID = "BasementKey";

    public void Interact()
    {

        Debug.Log("KEY INTERACT WORKS");
        
        Debug.Log("Picked key: " + keyID);

        

        InventorySystem.instance.AddKey(keyID);

        Destroy(gameObject);
    }

    public string GetPromptText()
    {
        return "Взять ключ";
    }
}