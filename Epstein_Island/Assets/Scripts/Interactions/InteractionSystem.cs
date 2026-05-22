using TMPro;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public float interactionDistance = 3f;
    public LayerMask interactableLayer;

    public GameObject interactionPrompt;
    public TextMeshProUGUI promptText;

    private Highlightable currentHighlight;

    private bool promptVisible = false;

    void Update()
    {
        if (Note.isUIOpen)
        {
            HidePrompt();
            return;
        }

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(transform.position,
            transform.forward * interactionDistance,
            Color.red);

        if (Physics.Raycast(ray, out hit,
            interactionDistance,
            interactableLayer))
        {
            Highlightable highlight =
                hit.collider.GetComponentInParent<Highlightable>();

            if (highlight != null)
            {
                if (currentHighlight != highlight)
                {
                    if (currentHighlight != null)
                        currentHighlight.UnHighlight();

                    highlight.Highlight();
                    currentHighlight = highlight;
                }
            }

            IInteractable interactable =
                hit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                ShowPrompt(interactable.GetPromptText());

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }

                return;
            }
        }

        if (currentHighlight != null)
        {
            currentHighlight.UnHighlight();
            currentHighlight = null;
        }

        HidePrompt();
    }

    void ShowPrompt(string text)
    {
        if (!promptVisible)
        {
            interactionPrompt.SetActive(true);
            promptVisible = true;
        }

        promptText.text = text;
    }

    void HidePrompt()
    {
        if (promptVisible)
        {
            interactionPrompt.SetActive(false);
            promptVisible = false;
        }
    }
}