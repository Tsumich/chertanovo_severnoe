using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;



public class InteractableObject : MonoBehaviour, IInteractable
{
    public string objectName = "ventilation";
    public int objectId;
    public string displayName = "Вентиляция";

    public string enterSceneName = "VentilationMiniGame";

    public ObjectType objectType;

    public string GetHintToInteract()
    {
        return "Нажмите E, чтобы взаимодействовать с <color=yellow>" + displayName + " </color>";
    }

    public void Interact()
    {
        if (objectType == ObjectType.EnterScene)
        {
            if (Inventory.Instance.HasItem("mop"))
            {
                Debug.Log("Швабра есть ");
                Messages.Instance.showDialogWindow("Почистить вентиляцию? ", () =>
                {
                    SceneManager.LoadScene(enterSceneName);
                    Messages.Instance.CloseDialogWindow();
                });
            }
            else
            {
                Messages.Instance.messageText.text = "Нечем воспользоваться для чистки";
                Debug.Log("Швабры нет");
            }
        }

        else if (objectType == ObjectType.OpenDoor) {
            // пока что переход в локации будет через двери

            if (QuestManager.Instance.activeQuest)
            {
                QuestSO active_quest = QuestManager.Instance.activeQuest;
                QuestObjective firstUncompleted = active_quest.objectives
                        .FirstOrDefault(objective => !objective.isCompleted);
                Debug.Log(firstUncompleted.triggerLocation);
                if (firstUncompleted != null && firstUncompleted.triggerLocation == this.objectId)
                {
                    Debug.Log("О тут квест на локу");
                    QuestManager.Instance.Complete_objective(active_quest, firstUncompleted);

                }

            }

            if (enterSceneName != null)
            {
                SceneManager.LoadScene(enterSceneName);
            }
        }
           
    }
}

public enum ObjectType
{
    EnterScene,    
    OpenDoor, 
}