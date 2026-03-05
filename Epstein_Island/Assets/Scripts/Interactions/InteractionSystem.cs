using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public float interactionDistance = 3f;
    public LayerMask interactableLayer;

    public GameObject interactionPrompt;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.red);

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            PickupItem pickup = hit.collider.GetComponent<PickupItem>();

            if (pickup != null)
            {
                interactionPrompt.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    pickup.Pickup();
                }
            }
            else
            {
                interactionPrompt.SetActive(false);
            }
        }
        else
        {
            interactionPrompt.SetActive(false);
        }
    }
}