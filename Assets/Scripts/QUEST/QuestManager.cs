using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public QuestSO activeQuest;

    public List<QuestSO> completed_quests = new List<QuestSO>();

    void Start()
    {
        RestoreActiveQuests();
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Восстанавливаем активные квесты при запуске
    private void RestoreActiveQuests()
    {
        QuestSO[] allQuests = Resources.FindObjectsOfTypeAll<QuestSO>();

        foreach (QuestSO quest in allQuests)
        {
            if (quest.currentState == QuestState.InProgress)
            {
                activeQuest = quest; // активный квест он только один!
                Debug.Log($"Восстановлен активный квест: {quest.questName}");
            }
            if (quest.currentState == QuestState.Completed)
            {
                completed_quests.Add(quest);
            }
        }

        if (activeQuest != null)
        {
            QuestHUDManager.Instance.ShowQuest(activeQuest);
        }
        else
        {
            QuestHUDManager.Instance.ClearHUD();
        }
        Debug.Log("В UI переданы информация о квесте и задаче");
    }

    public void AcceptQuest(QuestSO quest)
    {
        if (activeQuest == quest) return;

        quest.currentState = QuestState.InProgress;

        QuestObjective next_objective = quest.objectives
                    .FirstOrDefault(objective => !objective.isCompleted);

        QuestHUDManager.Instance.ShowQuest(quest);
        Debug.Log("accept quest");
        Debug.Log($"Принят квест: {quest.questName}");

        activeQuest = quest;
    }

    public void Complete_quest(QuestSO quest)
    {
        quest.currentState = QuestState.Completed;
        Inventory.Instance.AddCoins(quest.reward);
  
    }

    public void Complete_objective(QuestSO current_quest, QuestObjective objective)
    {
        objective.isCompleted = true;
        QuestObjective next_objective = current_quest.objectives
                        .FirstOrDefault(objective => !objective.isCompleted);
        if (next_objective == null)
        {
            Debug.Log("Квест завершен - все задачи выполнены");
            QuestHUDManager.Instance.ClearHUD();
        }
        else
        {
            QuestHUDManager.Instance.ShowQuest(current_quest);
            Inventory.Instance.AddCoins(current_quest.reward);
            Debug.Log("В UI переданы информация о квесте и задаче");
        }

    }
}