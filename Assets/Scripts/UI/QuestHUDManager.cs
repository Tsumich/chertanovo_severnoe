using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq; // ДОБАВЬТЕ в начало файла

public class QuestHUDManager : MonoBehaviour
{
    public static QuestHUDManager Instance;

    [Header("HUD Elements")]
    public TextMeshProUGUI locationText;
    public TextMeshProUGUI questTitleText;
    public TextMeshProUGUI questObjectiveText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        locationText.text = "Чертаново Северное: " + PlayerInteraction.Instance.lastLocation;
        CheckActiveQuestsOnStart();
    }

    // Проверяем активные квесты при запуске игры
    private void CheckActiveQuestsOnStart()
    {
        // Ждём немного чтобы PlayerQuestManager успел инициализироваться
        Invoke(nameof(CheckActiveQuests), 0.1f);
    }

    public void CheckActiveQuests()
    {
        if (QuestManager.Instance == null)
        {
            Debug.LogError("QuestManager не найден!");
            return;
        }

        Debug.Log($"Проверяем активные квесты. Всего: {QuestManager.Instance.activeQuests.Count}");

        // Ищем первый квест в статусе InProgress
        QuestSO activeQuest = QuestManager.Instance.activeQuests.Find(quest =>
            quest.currentState == QuestState.InProgress);

        if (activeQuest != null)
        {
            Debug.Log($"Найден активный квест: {activeQuest.questName}");

            ShowQuest(activeQuest);
        }
        else
        {
            Debug.Log("Активных квестов не найдено");
            // Очищаем HUD если нет активных квестов
            ClearHUD();
        }
    }

    public void ShowQuest(QuestSO quest )
    {
        QuestObjective firstUncompleted = quest.objectives
            .FirstOrDefault(objective => !objective.isCompleted);
        if (questTitleText != null)
        {
            questTitleText.text = quest.questName;
            questTitleText.gameObject.SetActive(true);
        }

        if (questObjectiveText != null)
        {
            questObjectiveText.text = firstUncompleted.description;
        }

        Debug.Log($"HUD: Показан квест '{quest.questName}'");
    }
 
    // Очищаем HUD
    private void ClearHUD()
    {
        if (questTitleText != null)
        {
            questTitleText.text = "";
            questTitleText.gameObject.SetActive(false);
        }

        if (questObjectiveText != null)
        {
            questObjectiveText.text = "";
            questObjectiveText.gameObject.SetActive(false);
        }
    }

    public void SetLocation(string locationName)
    {
        if (locationText != null)
            locationText.text = locationName;
    }
}