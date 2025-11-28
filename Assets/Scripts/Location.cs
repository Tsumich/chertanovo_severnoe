using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Location : MonoBehaviour
{
    public string locationName;
    public int id;
    public string enterLocation;

    private bool playerInRange = false;  // ДОЛЖЕН работать!

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("enterLocation: " + enterLocation);
            PlayerInteraction.Instance.lastLocation = enterLocation;
            SceneManager.LoadScene(enterLocation);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            // Для квестов сохраняем инфу о локации
            PlayerInteraction.Instance.lastLocation = this.enterLocation;

            if (QuestManager.Instance.activeQuest)
            {
                QuestSO active_quest = QuestManager.Instance.activeQuest;
                QuestObjective firstUncompleted = active_quest.objectives
                        .FirstOrDefault(objective => !objective.isCompleted);
                Debug.Log(firstUncompleted.triggerLocation);
                if (firstUncompleted != null && firstUncompleted.triggerLocation == this.id)
                {
                    Debug.Log("О тут квест на локу");
                    QuestManager.Instance.Complete_objective(active_quest, firstUncompleted);

                }

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
