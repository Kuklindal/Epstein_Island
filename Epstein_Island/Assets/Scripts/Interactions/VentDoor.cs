using UnityEngine;

public class VentDoor : MonoBehaviour, IInteractable
{
    public Transform ventDoor;

    public Vector3 openRotation = new Vector3(0, 0, -90);

    public float speed = 2f;

    private bool isOpen = false;

    private Quaternion closedRot;
    private Quaternion openedRot;

    void Start()
    {
        closedRot = ventDoor.localRotation;
        openedRot = Quaternion.Euler(openRotation);
    }

    void Update()
    {
        if (isOpen)
        {
            ventDoor.localRotation = Quaternion.Slerp(
                ventDoor.localRotation,
                openedRot,
                Time.deltaTime * speed
            );
        }
        else
        {
            
            ventDoor.localRotation = Quaternion.Slerp(
                ventDoor.localRotation,
                closedRot,
                Time.deltaTime * speed
            );
        }
    }

    public void Interact()
    {
        if (!isOpen)
        {
            isOpen = true;
            GoalUndeground.instance.CompleteObjective("Найти и открыть вентиляцию");
            ventDoor.GetComponent<Collider>().enabled = false;
        }
        else
        {
            isOpen = false;
        }
    }

    public string GetPromptText()
    {
        return isOpen ? "[E] Закрыть" : "[E] Открыть вентиляцию";
    }
}