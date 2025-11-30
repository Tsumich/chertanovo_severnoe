using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public QuestSO activeQuest;

    public delegate void QuestHandler(QuestSO quest);
    public event QuestHandler OnCompletedQuestEvent;

    public delegate void ObjectiveHandler(QuestObjective objective);
    public event ObjectiveHandler OnCompletedObjectiveEvent;

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
            HUDManager.Instance.ShowQuest(activeQuest);
        }
        else
        {
            HUDManager.Instance.ClearHUD();
        }
        Debug.Log("В UI переданы информация о квесте и задаче");
    }

    public void AcceptQuest(QuestSO quest)
    {
        if (activeQuest == quest) return;

        quest.currentState = QuestState.InProgress;

        QuestObjective next_objective = quest.objectives
                    .FirstOrDefault(objective => !objective.isCompleted);

        HUDManager.Instance.ShowQuest(quest);
        Debug.Log("accept quest");
        Debug.Log($"Принят квест: {quest.questName}");

        activeQuest = quest;
    }

    public void Complete_quest(QuestSO quest)
    {
        quest.currentState = QuestState.Completed;
        Inventory.Instance.AddCoins(quest.reward);
        this.OnCompletedQuestEvent?.Invoke(quest);
    }

    public void Complete_objective(QuestSO current_quest, QuestObjective objective)
    {
        objective.isCompleted = true;
        QuestObjective next_objective = current_quest.objectives
                        .FirstOrDefault(objective => !objective.isCompleted);
        if (next_objective == null)
        {
            Complete_quest(current_quest);
            Debug.Log("Квест завершен - все задачи выполнены");
            HUDManager.Instance.ClearHUD();
        }
        else
        {
            HUDManager.Instance.ShowQuest(current_quest);
            this.OnCompletedObjectiveEvent?.Invoke(next_objective);
            Debug.Log("В UI переданы информация о квесте и задаче");
        }

    }
}