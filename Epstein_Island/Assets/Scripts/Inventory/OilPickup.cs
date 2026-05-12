using UnityEngine;

public class OilPickup : MonoBehaviour, IInteractable
{
    public ItemData oilItem;

    public void Interact()
    {
        InventorySystem inventory = FindObjectOfType<InventorySystem>();

        if (inventory != null)
        {
            inventory.AddItem(oilItem);
            Destroy(gameObject);

            UIManager.instance.ShowMessage("Смазка найдена");

            
        }
    }

    public string GetPromptText()
    {
        return "Взять смазку";
    }
}