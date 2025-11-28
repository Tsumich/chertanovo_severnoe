using UnityEngine;


public class PickupItem : MonoBehaviour
{
    public string itemName = "mop";
    public string displayName = "Швабра";
    public Sprite itemIcon;
    //public Camera camera;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Messages.Instance.showDialogWindow("Взять с собой " + displayName);
        }
        if (playerInRange && Messages.Instance.isButtonPressed)
        {
            Inventory.Instance.AddItem(itemName, displayName, itemIcon);
            PlayerInteraction.Instance.hintPanel.SetActive(false);
            Debug.Log("Взяли предмет : " + displayName);
            Messages.Instance.CloseDialogWindow();
        }

    }


     void OnTriggerEnter(Collider other)
     {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            //Debug.Log($"Подойди к {npcName} - нажми E");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            //Debug.Log($"Отошли от {npcName}");
        }
    }
}