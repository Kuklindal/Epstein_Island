using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    public ItemData item;

    private InventorySystem inventory;

    void Start()
    {
        inventory = FindObjectOfType<InventorySystem>();

        if (inventory == null)
            Debug.LogError("InventorySystem not found in scene!");
    }

    public void Interact()
    {
        Debug.Log("PickupItem Interact called: " + gameObject.name);

        if (inventory == null)
        {
            Debug.LogError("Cannot pick up item: InventorySystem is missing!");
            return;
        }

        if (item == null)
        {
            Debug.LogError("Cannot pick up item: ItemData is missing!");
            return;
        }

        inventory.AddItem(item);
        Destroy(gameObject);
    }

    public string GetPromptText()
    {
        if (item == null)
            return "Pick up item";

        return "Pick up " + item.itemName;
    }
}