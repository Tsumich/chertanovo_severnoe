using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

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
    }

    public void ShowQuest(QuestSO quest)
    {
        QuestObjective next_objective = quest.objectives
                .FirstOrDefault(objective => !objective.isCompleted);
        if (next_objective == null)
        {
            Debug.Log("Квест завершен - все задачи выполнены");
            QuestHUDManager.Instance.ClearHUD();
            return;
        }
        if (questTitleText != null)
        {
            questTitleText.text = quest.questName;
            questTitleText.gameObject.SetActive(true);
        }

        if (questObjectiveText != null)
        {
            questObjectiveText.text = next_objective.description;
        }

        Debug.Log($"HUD: Показан квест '{quest.questName}'");
    }

    public void ClearHUD()
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