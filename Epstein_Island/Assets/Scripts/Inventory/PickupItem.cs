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
        if (item.itemName == "Ключ от подвала")
            GoalVilla.instance.CompleteObjective("Найти ключ");
        if (item.itemName == "Ключ от архива")
        {
            GoalUndeground.instance.CompleteObjective("Взять ключ");
        }
        Destroy(gameObject);
    }

    public string GetPromptText()
    {
        return "[E] Взять ключ";
    }
}