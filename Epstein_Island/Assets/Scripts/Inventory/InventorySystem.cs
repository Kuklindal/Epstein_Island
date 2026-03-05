using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public List<ItemData> items = new List<ItemData>();

    public void AddItem(ItemData item)
    {
        items.Add(item);

        Debug.Log("Item added: " + item.itemName);
    }

    public bool HasItem(ItemData item)
    {
        return items.Contains(item);
    }
}