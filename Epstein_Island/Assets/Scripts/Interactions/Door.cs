using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public bool isOpen = false;

    [Header("Lock Settings")]
    public bool isLocked = false;
    public bool requiresKey = false;
    public bool requiresHack = false;

    public ItemData requiredKey;

    [Header("Animation")]
    public float openAngle = 90f;
    public float openSpeed = 2f;

    [Header("Hack")]
    public HackMinigame hackUI;

    [Header("Scene Transition")]
    public bool loadSceneOnOpen = false;
    public string sceneToLoad;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    public void Interact()
    {
        Debug.Log("Door interact");

        InventorySystem inventory = FindObjectOfType<InventorySystem>();

        // 🔐 КЛЮЧ
        if (requiresKey)
        {
            if (inventory == null)
            {
                Debug.LogError("InventorySystem not found!");
                return;
            }

            if (!inventory.HasItem(requiredKey))
            {
                Debug.Log("Нужен ключ");
                return;
            }

            isLocked = false;
        }

        // 🔧 ВЗЛОМ
        if (requiresHack)
        {
            GoalVilla.instance.CompleteObjective("Найти секретную дверь");
            HackDoor();
            return;
        }

        // 🔒 ОБЫЧНАЯ БЛОКИРОВКА
        if (isLocked)
        {
            Debug.Log("Дверь заперта");
            return;
        }

        // 🚪 ОТКРЫТИЕ
        if (!isOpen)
        {
            OpenDoor();
        }
        else
        {
            isOpen = false;
        }
    }

    void OpenDoor()
    {
        isOpen = true;

        // 🔥 ПЕРЕХОД НА СЦЕНУ
        if (loadSceneOnOpen)
        {
            SceneTransition transition = FindObjectOfType<SceneTransition>();

            if (transition != null)
            {
                transition.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.LogError("SceneTransition not found!");
            }
        }
    }

    void HackDoor()
    {
        if (hackUI.gameObject.activeSelf)
            return;

        hackUI.StartGame();

        hackUI.onSuccess = () =>
        {
            GoalVilla.instance.CompleteObjective("Взломать секретную дверь");
            requiresHack = false;
            isLocked = false;

            OpenDoor(); // 🔥 сразу открываем + переход
        };
    }

    void Update()
    {
        if (isOpen)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, openRotation, Time.deltaTime * openSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, closedRotation, Time.deltaTime * openSpeed);
        }
    }

    public string GetPromptText()
    {
        InventorySystem inventory = FindObjectOfType<InventorySystem>();

        if (requiresKey)
        {
            if (inventory != null && inventory.HasItem(requiredKey))
                return "[E] Открыть (ключ)";
            else
                return "Нужен ключ";
        }

        if (requiresHack)
            return "[E] Взломать";

        if (isLocked)
            return "Закрыто";

        return isOpen ? "[E] Закрыть" : "[E] Открыть";
    }
}