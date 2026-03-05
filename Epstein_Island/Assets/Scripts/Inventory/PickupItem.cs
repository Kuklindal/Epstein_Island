using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public ItemData item;

    private InventorySystem inventory;

    void Start()
    {
        inventory = FindObjectOfType<InventorySystem>();
    }

    public void Pickup()
    {
        inventory.AddItem(item);

        Destroy(gameObject);
    }
}