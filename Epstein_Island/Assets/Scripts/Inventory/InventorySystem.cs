using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;

    public List<ItemData> items = new List<ItemData>();

    private HashSet<string> keys = new HashSet<string>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(ItemData item)
    {
        if (item == null)
        {
            Debug.LogWarning("Trying to add null item");
            return;
        }

        if (!items.Contains(item))
        {
            items.Add(item);
            Debug.Log("Item added: " + item.itemName);
        }
    }

    public bool HasItem(ItemData item)
    {
        return item != null && items.Contains(item);
    }

    // Новая логика для ключей
    public void AddKey(string keyID)
    {
        if (string.IsNullOrEmpty(keyID))
        {
            Debug.LogWarning("Key ID is empty");
            return;
        }

        keys.Add(keyID);
        Debug.Log("Key added: " + keyID);
    }

    public bool HasKey(string keyID)
    {
        if (string.IsNullOrEmpty(keyID))
            return false;

        return keys.Contains(keyID);
    }
}