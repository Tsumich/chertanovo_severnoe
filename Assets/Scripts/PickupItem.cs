using UnityEngine;


public class PickupItem : MonoBehaviour
{
    public string itemName = "mop";
    public string displayName = "Швабра";
    public Sprite itemIcon;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Inventory.Instance.AddItem(itemName, displayName, itemIcon);
            PlayerInteraction.Instance.itemIsTaken = true;
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