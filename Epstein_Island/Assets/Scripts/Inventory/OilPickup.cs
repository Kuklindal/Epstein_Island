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
            GoalUndeground.instance.CompleteObjective("Найти смазку");
            //UIManager.instance.ShowMessage("Смазка найдена");
            

        }
    }

    public string GetPromptText()
    {
        return "[E] Взять смазку";
    }
}