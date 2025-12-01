using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInteract : MonoBehaviour

{
    public Camera mainCamera;
    public float interactionDistance = 10f;

    public GameObject interactionPanel;
    public TextMeshProUGUI interactionText;

    void Update()
    {
         InteractionRay();
    }

    void InteractionRay()
    {
        Ray ray = mainCamera.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;
         
        if (DialogueManager.Instance.isInDialogue)
        {
            interactionPanel.SetActive(false);
            return;
        }

        bool hitSomething = false;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if(interactable != null)
            {
                hitSomething = true;
                interactionText.text = interactable.GetHintToInteract();
                if (Input.GetKeyDown(KeyCode.E))
                {      
                    interactable.Interact();
                }
            }
        }
        interactionPanel.SetActive(hitSomething);
    }
}