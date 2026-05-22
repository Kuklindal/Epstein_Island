using UnityEngine;

public class GeneratorLever : MonoBehaviour, IInteractable
{
    public ItemData lubricantItem;

    public Transform leverHandle;

    public float downAngle = -60f;
    public float speed = 2f;

    private bool isLubricated = false;
    private bool isActivated = false;
    public AudioSource generatorAudio;
    private Quaternion startRotation;
    private Quaternion targetRotation;

    void Start()
    {
        startRotation = leverHandle.localRotation;
        targetRotation = Quaternion.Euler(
            leverHandle.localEulerAngles + new Vector3(downAngle, 0, 0)
        );
    }

    public void Interact()
    {
        InventorySystem inventory = FindObjectOfType<InventorySystem>();

        // ЕСЛИ РЫЧАГ ЕЩЁ НЕ СМАЗАН
        if (!isLubricated)
        {
            // Проверяем есть ли масло
            if (inventory != null && inventory.HasItem(lubricantItem))
            {
                isLubricated = true;

                UIManager.instance.ShowMessage("Вы смазали рычаг");

                return;
            }
            else
            {
                GoalUndeground.instance.AddObjective("Найти смазку");
                UIManager.instance.ShowMessage("Рычаг заклинило");
                return;
            }
        }

        // ЕСЛИ УЖЕ ВКЛЮЧЕН
        if (isActivated)
            return;

        // ВКЛЮЧАЕМ ПИТАНИЕ
        isActivated = true;
        generatorAudio.Play();
        //UIManager.instance.ShowMessage("Питание восстановлено");
        GoalUndeground.instance.CompleteObjective("Включить генератор");
        FindObjectOfType<PowerSystem>().RestorePower();
    }

    void Update()
    {
        if (isActivated)
        {
            leverHandle.localRotation = Quaternion.Lerp(
                leverHandle.localRotation,
                targetRotation,
                Time.deltaTime * speed
            );
        }
    }

    public string GetPromptText()
    {
        InventorySystem inventory = FindObjectOfType<InventorySystem>();

        if (!isLubricated)
        {
            if (inventory != null && inventory.HasItem(lubricantItem))
                return "[E] Смазать рычаг";

            return "Рычаг заклинило";
        }

        if (!isActivated)
            return "[E] Включить питание";

        return "";
    }
}