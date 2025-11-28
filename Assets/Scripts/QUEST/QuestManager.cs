using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public List<QuestSO> activeQuests = new List<QuestSO>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            RestoreActiveQuests();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Восстанавливаем активные квесты при запуске
    private void RestoreActiveQuests()
    {
        activeQuests.Clear();

        // Находим ВСЕ QuestSO в проекте
        QuestSO[] allQuests = Resources.FindObjectsOfTypeAll<QuestSO>();

        foreach (QuestSO quest in allQuests)
        {
            // Если квест в процессе - добавляем в активные
            if (quest.currentState == QuestState.InProgress)
            {
                activeQuests.Add(quest);
                Debug.Log($"Восстановлен активный квест: {quest.questName}");
            }
        }

        Debug.Log($"Всего восстановлено активных квестов: {activeQuests.Count}");
    }

    public void AcceptQuest(QuestSO quest)
    {
        if (activeQuests.Contains(quest)) return;

        quest.currentState = QuestState.InProgress;
        activeQuests.Add(quest);

        QuestHUDManager.Instance.ShowQuest(quest);
        Debug.Log("accept quest");
        Debug.Log($"Принят квест: {quest.questName}. Всего активных: {activeQuests.Count}");
    }

    public void Complete_quest(QuestSO quest)
    {
        quest.currentState = QuestState.Completed;
        Inventory.Instance.GetCoins(quest.reward);
        // Квест -менеджер вызывает событие и на него реагируют подписавшиеся. то есть HUD менеджер
    }
}