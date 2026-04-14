using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    public float interactionDistance = 3f;
    public LayerMask interactableLayer;

    public GameObject interactionPrompt;
    public Text promptText;
    Highlightable currentHighlight;
    void Update()
    {
        if (Note.isUIOpen)
        {
            interactionPrompt.SetActive(false);
            return;
        }
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.red);

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            Highlightable highlight = hit.collider.GetComponentInParent<Highlightable>();

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
            IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                interactionPrompt.SetActive(true);

                promptText.text = interactable.GetPromptText();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }

                return;
            }
        }
        else
        {
            if (currentHighlight != null)
            {
                currentHighlight.UnHighlight();
                currentHighlight = null;
            }
        }

        interactionPrompt.SetActive(false);
    }
}