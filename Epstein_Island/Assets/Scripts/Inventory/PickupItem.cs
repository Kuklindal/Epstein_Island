using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    public ItemData item;

    private InventorySystem inventory;

    void Start()
    {
        inventory = FindObjectOfType<InventorySystem>();
    }

    public void Interact()
    {
        inventory.AddItem(item);
        Destroy(gameObject);
    }

    public string GetPromptText()
    {
        return "Pick up " + item.itemName;
    }
}