using UnityEngine;

public class MorgueTray : MonoBehaviour, IInteractable
{
    [Header("Movement")]
    public Transform tray;
    public float slideDistance = 1.2f;
    public float speed = 2f;

    private Vector3 closedPosition;
    private Vector3 openPosition;

    private bool isOpen = false;
    private bool isMoving = false;

    void Start()
    {
        closedPosition = tray.localPosition;
        openPosition = closedPosition + new Vector3(0, 0, -slideDistance);
    }

    void Update()
    {
        if (isMoving)
        {
            Vector3 target = isOpen ? openPosition : closedPosition;

            tray.localPosition = Vector3.Lerp(
                tray.localPosition,
                target,
                Time.deltaTime * speed
            );

            if (Vector3.Distance(tray.localPosition, target) < 0.01f)
            {
                tray.localPosition = target;
                isMoving = false;
            }
        }
    }

    public void Interact()
    {
        if (isMoving) return;

        isOpen = !isOpen;
        isMoving = true;
    }

    public string GetPromptText()
    {
        return isOpen ? "[E] Задвинуть кушетку" : "[E] Выдвинуть кушетку";
    }
}