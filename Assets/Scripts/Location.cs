using UnityEngine;
using System.Linq; // ДОБАВЬТЕ в начало файла
using UnityEngine.SceneManagement;

public class Location : MonoBehaviour
{
    public string locationName;
    public int id;

    public string enterLocation;

    void Update()
    {
    
        if (PlayerInteraction.Instance.lastLocation == enterLocation && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Загружаем сцену: ");
            Debug.Log(this.enterLocation);
            SceneManager.LoadScene(this.enterLocation);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInteraction.Instance.lastLocation = this.enterLocation;

            if (QuestManager.Instance.activeQuests.Count > 0)
            {
                QuestSO activeQuest = QuestManager.Instance.activeQuests.Find(quest =>
                    quest.currentState == QuestState.InProgress);
                QuestObjective firstUncompleted = activeQuest.objectives
                        .FirstOrDefault(objective => !objective.isCompleted);
                Debug.Log(firstUncompleted.triggerLocation);
                if (firstUncompleted != null && firstUncompleted.triggerLocation == this.id && firstUncompleted.relatedNPCs == 0)

                {
                    Debug.Log("О тут квест на локу");
                    firstUncompleted.isCompleted = true;
                    QuestHUDManager.Instance.CheckActiveQuests();
                }

            }
        }
    }

}
