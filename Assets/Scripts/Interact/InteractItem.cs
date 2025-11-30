using UnityEngine;


public class InteractItem : MonoBehaviour, IInteractable
{
    public string itemName = "mop";
    public string displayName = "Швабра";

    public itemType itemType;
    public ThroughtsSO throughts;

    public Sprite itemIcon;


    public void Interact()
    {
        if (itemType == itemType.PickUpItem)
        {
            Messages.Instance.showDialogWindow("Взять с собой " + displayName, () =>
            {
                Inventory.Instance.AddItem(itemName, displayName, itemIcon);
                Debug.Log("Взяли предмет : " + displayName);
            });
        }
        else if (itemType == itemType.InspectItem)
        {
            DialogueManager.Instance.StartDialogue(throughts);
        }
        else if (itemType == itemType.PickUpItem)
        {
            Messages.Instance.showDialogWindow("Взять с собой " + displayName, () =>
            {
                Inventory.Instance.AddItem(itemName, displayName, itemIcon);
                Debug.Log("Взяли предмет : " + displayName);
            });
        }
        else return;
    }

    public string GetDescription()
    {
        return "Нажмите E, чтобы подобрать предмет";
    }

    public string GetHintToInteract()
    {
        if (itemType == itemType.PickUpItem) return "Нажмите E, чтобы подобрать предмет";
        else if (itemType == itemType.InteractItem) return $"Нажмите E, чтобы взаимодействаоть с <color=yellow> {displayName} </color>";
        else if (itemType == itemType.InspectItem) return $"Нажмите E, чтобы проверить: <color=yellow> {displayName} </color>";
        else return "Эта что?";
    }
}

public enum itemType
{
    PickUpItem,   // предмет для взятия   (швабра)
    InteractItem, // чота получить с него (кофейный автомат)
    InspectItem,   // просто посмотреть ( компутер)
}